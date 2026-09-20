import { debounceTime, distinctUntilChanged, filter, switchMap, finalize, mergeMap } from 'rxjs/operators';
import { IEntity, ILookup } from "../interfaces";
import { UntypedFormGroup } from '@angular/forms';
import { HttpService } from '../services/http.service';
import { IBaseForm } from '../components/base-form.component';

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

    apiService?: HttpService;
    isError: boolean = false;
    validationForm?: UntypedFormGroup;
    form?: IBaseForm;
    result?: any;
    isForm: boolean = false;
    isFilter: boolean = false;

    queryUrl: string = '';
    id?: string = '';
    name?: string = '';
    isInitial?: boolean = true;
    showDropDown: boolean = false;
    options: ILookup[] = [];
    isLoading: boolean = false;
    fieldName: string = '';
    control: any = null;

    constructor(autoComplete: IAutoComplete) {
        this.queryUrl = autoComplete.queryUrl;
        this.id = autoComplete.id;
        this.name = autoComplete.name;
        this.isInitial = autoComplete.isInitial;
        this.showDropDown = autoComplete.showDropDown;
        this.options = autoComplete.options;
        this.isLoading = autoComplete.isLoading;
        this.fieldName = autoComplete.fieldName;
        this.control = autoComplete.control;
    }
    //---------------------------------------------------------
    async setUpForm(apiService: HttpService, form: IBaseForm) {
        this.apiService = apiService;
        this.form = form;
        this.isForm = true;
        // set event handler for the validationForm input-change event, of the autoControl field
        let controlName = this.fieldName + 'Name';
        this.form.validationForm.get(controlName)?.valueChanges.pipe(
            // 1. Only proceed if input length >= 2
            filter(val => val && val.length >= 2),
            // 2. Wait 300ms after last keystroke to avoid API spam
            debounceTime(300),
            // 3. Only trigger if the value actually changed
            distinctUntilChanged(),
            // 4. Switch to API call
            switchMap(
                async (searchTerm) => {
                    if (!this.isInitial) {
                        return await this.doApiCall(searchTerm);  // returning result
                    }
                    else {
                        this.isInitial = false;
                        return null;
                    }
                }
            )
        ).subscribe(result => {/*No need to do anything here*/ });
    }
    //---------------------------------------------------------  
    setFormControls() {
        //set the validationForm controls
        let controlId = this.fieldName + 'Id';
        let controlName = this.fieldName + 'Name';

        this.form?.validationForm.get(controlId)?.setValue(this.id, { emitEvent: false });
        this.form?.validationForm.get(controlName)?.setValue(this.name, { emitEvent: false });
    }
    //--------------------------------------------------------- 
    setupFilter(apiService: HttpService, result: any) {
        this.apiService = apiService;
        this.isFilter = true;
        this.result = result;
    }
    //--------------------------------------------------------- 
    setFilterControls() {
        //set the validationForm controls
        let controlId = this.fieldName + 'Id';
        let controlName = this.fieldName + 'Name';

        this.result[controlId] = this.id;
        this.result[controlName] = this.name;
    }
    //--------------------------------------------------------- 
    setData(id?: string, name?: string) {
        this.id = id;
        this.name = name;
        if (this.isForm) {
            this.setFormControls();
        }
        else {
            this.setFilterControls();
        }
    }
    //---------------------------------------------------------  
    cssClass() {
        let invalidSelection = this.id == undefined;
        let css = invalidSelection || this.isError ? 'is-invalid' : 'is-valid';
        css = this.form?.submit ? css : '';
        return css;
    }
    //---------------------------------------------------------
    onSelect(selectedOption: ILookup) {

        this.init();
        this.setData(selectedOption.id.toString(), selectedOption.name);
    }
    //---------------------------------------------------------

    async doAuto(searchTerm?: string): Promise<void> {
        const term = (searchTerm ?? '').trim();
        if (term.length < 2) {
            this.init();
            return;
        }

        this.isInitial = false;
        await this.doApiCall(searchTerm);  // returning result
    }
    //---------------------------------------------------------   

    async doApiCall(searchTerm?: string, messageList?: any[]): Promise<IAutoComplete> {
        this.isLoading = true;
        this.isError = false;
        try {
            const request = { pageSize: 20, filter: { name: searchTerm } };
            const response: any = await this.apiService?.postAutoComplete(this.queryUrl, request);
            this.options = response?.items ?? [];
            this.isError = response?.items.length == 0;
            this.setData(undefined, searchTerm);  //invalidate id, leave name as it is
        } catch (err: any) {
            messageList?.push({ text: err.msg || "Error fetching data", msgType: "danger" });
        } finally {
            this.isLoading = false;
            this.showDropDown = true;
        }

        return this as IAutoComplete;
    }
    //---------------------------------------------------------    
    isShowList(): boolean {
        return (!this.isInitial) && this.showDropDown && this.options.length > 0;
    }
    //---------------------------------------------------------
    onFocus() {
        (this.name || '').length >= 2 ? this.showDropDown = true : this.showDropDown = false;
    }
    //---------------------------------------------------------
    onInputChange(value: any,): void {
      //  const searchTerm = value ?? '';
        // Reset selected ID
        this.init();
      //  this.doAuto(val);
        const searchTerm = (value ?? '').trim();
        if (searchTerm.length < 2) {
            this.init();
            return;
        }
        
        this.isInitial = false;
        this.doApiCall(searchTerm);  // returning result
    }
    //---------------------------------------------------------
    init() {
        this.name = undefined;
        this.id = undefined;
        this.options = [];
        this.showDropDown = false;
        this.isInitial = true;
    }
    //---------------------------------------------------------
    hideOverlay() {
        setTimeout(() => this.showDropDown = false, 200);
    }
    //---------------------------------------------------------
    get optionList() {
        return this.options;
    }
    //---------------------------------------------------------
}