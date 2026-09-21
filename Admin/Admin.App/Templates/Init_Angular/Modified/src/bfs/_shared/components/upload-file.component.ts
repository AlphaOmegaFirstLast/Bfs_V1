import { ChangeDetectionStrategy, Component, signal, inject } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { NgbProgressbarModule } from '@ng-bootstrap/ng-bootstrap';
import { CommonModule } from '@angular/common';
import { HttpClient, HttpEventType, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { TokenService } from '../security/token.service';

@Component({
  selector: 'app-upload',
  imports: [CommonModule, NgbProgressbarModule],
  templateUrl: './upload-file.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class UploadFileComponent {

  public uploadUrl = ''; //'https://localhost:2100/api/ActionType/upload';
  public result :string = '';
  public parent: any;
  // Dependency Injection
  http: HttpClient;

  // Signals for state management
  selectedFile = signal<File | null>(null);
  uploadStatus = signal<'Pending' | 'Success' | 'Error' | null>(null);
  statusMessage = signal<string>('');
  uploadProgress = signal<number>(0);
  isUploading = signal<boolean>(false);
  tokenService: TokenService;

  //-------------------------------------

  constructor(public activeModal: NgbActiveModal) {
    this.http = inject(HttpClient);
    this.tokenService = inject(TokenService);
  }
  //---------------------------------------------------------
  cancel() {
    this.activeModal.close('Cancel');
  }
   //---------------------------------------------------------
  upload() {
    this.onUpload();
  } 
  //---------------------------------------------------------  
  /**
   * Captures the selected file from the input event.
   * @param event The file input change event.
   */
  onFileSelected(event: Event): void {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files.length > 0) {
      const file = input.files[0];
      // Basic check to ensure it looks like a JSON file, as per the curl example
      if (file.type === 'application/json' || file.name.toLowerCase().endsWith('.json')) {
        this.selectedFile.set(file);
        // Reset status on new file selection
        this.uploadStatus.set(null);
        this.statusMessage.set('');
        this.uploadProgress.set(0);
      } else {
        this.selectedFile.set(null);
        this.uploadStatus.set('Error');
        this.statusMessage.set('Please select a valid JSON file.');
      }
    } else {
      this.selectedFile.set(null);
    }
  }
  //---------------------------------------------------------
  /**
   * Executes the file upload to the server.
   */
  async onUpload(): Promise<void> {
    const file = this.selectedFile();
    if (!file) {
      this.uploadStatus.set('Error');
      this.statusMessage.set('No file selected for upload.');
      return;
    }

    this.isUploading.set(true);
    this.uploadStatus.set('Pending');
    // String concatenation is used here and below to avoid template literal compilation errors
    this.statusMessage.set('Uploading "' + file.name + '" to ' + this.uploadUrl + '...');
    this.uploadProgress.set(0);

    // 1. Create FormData object
    const formData = new FormData();
    // 2. Append the file with the field name 'file', as required by the endpoint/curl command
    // The third argument is the filename, which helps the server correctly identify the file.
    formData.append('file', file, file.name);
    // 3. Prepare headers, including Authorization if needed
    let headers = new HttpHeaders();
       const accessToken = await this.tokenService.getToken();
    if (accessToken) {
      headers = headers.set('Authorization', 'Bearer ' + accessToken);
    }

    this.http.post(this.uploadUrl, formData, {
      reportProgress: true, // Report upload progress events
      observe: 'events',     // Observe all events, not just the final response
      headers: headers,
      withCredentials: false // Ensure credentials are not sent, as we're using token-based auth
    }).subscribe({
      next: (event) => {
        switch (event.type) {
          case HttpEventType.UploadProgress:
            // Calculate and update the progress percentage
            const percentDone = Math.round(100 * event.loaded / (event.total || file.size));
            this.uploadProgress.set(percentDone);
            break;

          case HttpEventType.Response:
            // Upload complete, final response received
            this.uploadStatus.set('Success');
            this.statusMessage.set('Successfully uploaded file! Server responded with status ' + event.status + '.');
            this.isUploading.set(false);
            // In a real application, you might want to log or display event.body here
            console.log('Server response:', event.body);
            //-------------------
            this.activeModal.close('Upload');
            this.parent.onImportList();
            //-------------------
            break;
        }
      },
      error: (error: HttpErrorResponse) => {
        this.isUploading.set(false);
        this.uploadStatus.set('Error');

        if (error.status === 0) {
          this.statusMessage.set('Upload failed: Could not connect to the server at ' + this.uploadUrl + '. Please ensure the server is running.');
        } else {
          if (error.error instanceof ErrorEvent) {
            // Client-side error
            this.statusMessage.set('Upload failed with client-side error: ' + error.error.message);
          } else {
            // Server-side error
            if (error.error && error.error.detail) {
              this.statusMessage.set('Upload failed with server error ' + error.status + ': ' + error.error.detail);
            } else {  
              this.statusMessage.set('Upload failed with error ' + error.status + ': ' + (error.statusText || 'Unknown error') + '. Check the console for details.');
            }           
          }
        }
        console.error('Upload error:', error);
      },
      complete: () => {
        this.isUploading.set(false);
        // If the response event hasn't set success (e.g., if we only observe Response and not all events), ensure cleanup.
        if (this.uploadStatus() === 'Pending') {
          this.uploadStatus.set('Success');
          this.statusMessage.set('Upload process completed.');
        }
      }
    });
  }
}