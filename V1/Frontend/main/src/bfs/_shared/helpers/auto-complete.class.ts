import { debounceTime, distinctUntilChanged, filter, switchMap, finalize, mergeMap } from 'rxjs/operators';
import { IEntity, ILookup } from "../interfaces";
import { UntypedFormGroup } from '@angular/forms';
import { HttpService } from '../services/http.service';

export interface IAutoComplete {
    queryUrl: string,
    id?: string,
    name?: string,
    isInitial?: boolean,
    showDropDown: boolean,
    options: ILookup[],
    isLoading: boolean,
    fieldName: string,
    control: any
}

export class AutoCompleteHelper {
    isError: boolean = false;
    validationForm?: UntypedFormGroup;
    constructor(public apiService: HttpService, public autoComplete: IAutoComplete) {
    }
    //---------------------------------------------------------
    async setOnChangeHandler(validationForm: UntypedFormGroup) {
        this.validationForm = validationForm;
        // set event handler for the validationForm input-change event, of the autoControl field
        let controlName = this.autoComplete.fieldName + 'Name';
        this.validationForm.get(controlName)?.valueChanges.pipe(
            // 1. Only proceed if input length >= 2
            filter(val => val && val.length >= 2),
            // 2. Wait 300ms after last keystroke to avoid API spam
            debounceTime(300),
            // 3. Only trigger if the value actually changed
            distinctUntilChanged(),
            // 4. Switch to API call
            switchMap(
                async (searchTerm) => {
                    if (!this.autoComplete.isInitial) {
                        return await this.doApiCall(searchTerm);  // returning result
                    }
                    else {
                        this.autoComplete.isInitial = false;
                        return null;
                    }
                }
            )
        ).subscribe(result => {/*No need to do anything here*/ });
    }
    //---------------------------------------------------------  
    setFormControls() {
        //set the validationForm controls
        let controlId = this.autoComplete.fieldName + 'Id';
        let controlName = this.autoComplete.fieldName + 'Name';

        this.validationForm?.get(controlId)?.setValue(this.autoComplete.id, { emitEvent: false });
        this.validationForm?.get(controlName)?.setValue(this.autoComplete.name, { emitEvent: false });
    }
    //--------------------------------------------------------- 
    setData(id?: string, name?: string) {
        this.autoComplete.id = id;
        this.autoComplete.name = name;
        this.setFormControls();
    }
    //---------------------------------------------------------  

    onSelect(selectedOption: ILookup) {

        this.init();
        this.setData(selectedOption.id.toString(), selectedOption.name);
    }
    //---------------------------------------------------------

    async doAuto(searchTerm?: string): Promise<void> {
        searchTerm = this.autoComplete.name ?? '';
        const term = (searchTerm ?? '').trim();
        if (term.length < 2) {
            this.init();
            return;
        }

        await this.doApiCall(searchTerm);
    }
    //---------------------------------------------------------   

    async doApiCall(searchTerm?: string, messageList?: any[]): Promise<IAutoComplete> {
        this.autoComplete.isLoading = true;
        this.isError = false;
        try {
            const request = { pageSize: 20, filter: { name: searchTerm } };
            const response: any = await this.apiService.postAutoComplete(this.autoComplete.queryUrl, request);
            this.autoComplete.options = response?.items ?? [];
            this.isError = response?.items.length == 0;
            this.setData(undefined, searchTerm);  //invalidate id, leave name as it is
        } catch (err: any) {
            messageList?.push({ text: err.msg || "Error fetching data", msgType: "danger" });
        } finally {
            this.autoComplete.isLoading = false;
            this.autoComplete.showDropDown = true;
        }

        return this.autoComplete;
    }
    //---------------------------------------------------------    
    isShowSpin(): boolean {
        return this.autoComplete.isLoading;
    }
    //---------------------------------------------------------

    isShowList(): boolean {
        return (!this.autoComplete.isInitial) && this.autoComplete.showDropDown && this.autoComplete.options.length > 0;
    }
    //---------------------------------------------------------
    onFocus() {
        (this.autoComplete.name || '').length >= 2 ? this.autoComplete.showDropDown = true : this.autoComplete.showDropDown = false;
    }
    //---------------------------------------------------------
    onInputChange(value: any,): void {
        const val = value ?? '';
        // Reset selected ID
        this.init();
        this.doAuto(val);
    }
    //---------------------------------------------------------
    init() {
        this.autoComplete.name = undefined;
        this.autoComplete.id = undefined;
        this.autoComplete.options = [];
        this.autoComplete.showDropDown = false;
        this.autoComplete.isInitial = true;
    }
    //---------------------------------------------------------
    hideOverlay() {
        setTimeout(() => this.autoComplete.showDropDown = false, 200);
    }
    //---------------------------------------------------------
    cssClass() {
       let invalidSelection = this.autoComplete.id == undefined;
    
       let css =  invalidSelection || this.isError ? 'is-invalid': 'is-valid';
       return css;
    }
    //---------------------------------------------------------
    get optionList() {
        return this.autoComplete.options;
    }
    //---------------------------------------------------------
}