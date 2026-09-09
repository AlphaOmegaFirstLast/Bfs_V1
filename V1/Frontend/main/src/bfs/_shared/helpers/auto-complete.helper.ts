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

export async function setAuto(apiService: HttpService, validationForm: UntypedFormGroup, entity: IEntity, autoComplete: IAutoComplete) {

    // set event handler for the validationForm input-change event, of the autoControl field
    let controlName = autoComplete.fieldName + 'Name';
    autoComplete.control = validationForm.get(controlName) as any;
    validationForm.get(controlName)?.valueChanges.pipe(
        // 1. Only proceed if input length >= 2
        filter(val => val && val.length >= 2),
        // 2. Wait 300ms after last keystroke to avoid API spam
        debounceTime(300),
        // 3. Only trigger if the value actually changed
        distinctUntilChanged(),
        // 4. Switch to API call
        switchMap(
            async (searchTerm) => {
                if (!autoComplete.isInitial) {
                    return await doAutoCall(apiService, autoComplete, searchTerm);  // returning result
                }
                else {
                    autoComplete.isInitial = false;
                    return null;
                }
            }
        )
    ).subscribe(result => {/*No need to do anything here*/ });
}
//---------------------------------------------------------    
export function onAutoSelect(validationForm: UntypedFormGroup, autoComplete: IAutoComplete, selectedOption: ILookup) {

    initAuto(autoComplete);
    // pickup selection into object
    autoComplete.id = selectedOption.id.toString();
    autoComplete.name = selectedOption.name;

    //set the validationForm controls
    let controlId = autoComplete.fieldName + 'Id';
    let controlName = autoComplete.fieldName + 'Name';

    validationForm.get(controlId)?.setValue(autoComplete.id, { emitEvent: false });
    validationForm.get(controlName)?.setValue(autoComplete.name, { emitEvent: false });
}
//---------------------------------------------------------

export async function doAuto(apiService: HttpService, autoComplete: IAutoComplete, searchTerm?: string): Promise<void> {
    searchTerm = autoComplete.name ?? '';
    const term = (searchTerm ?? '').trim();
    if (term.length < 2) {
        initAuto(autoComplete);
        return;
    }

    await doAutoCall(apiService, autoComplete, searchTerm);
}
//---------------------------------------------------------    
export async function doAutoCall(apiService: HttpService, autoComplete: IAutoComplete, searchTerm?: string, messageList?:any[]): Promise<IAutoComplete> {
    autoComplete.isLoading = true;
    try {
        const request = { pageSize: 20, filter: { name: searchTerm } };
        const response: any = await apiService.postAutoComplete(autoComplete.queryUrl, request);
        autoComplete.options = response?.items ?? [];
    } catch (err: any) {
        messageList?.push({ text: err.msg || "Error fetching data", msgType: "danger" });
    } finally {
        autoComplete.isLoading = false;
        autoComplete.showDropDown = true;
    }

    return autoComplete;
}
//---------------------------------------------------------    
export function isAutoShowSpin(autoComplete: IAutoComplete): boolean {
    return autoComplete.isLoading;
}
//---------------------------------------------------------
export function isAutoShowList(autoComplete: IAutoComplete): boolean {
    return (!autoComplete.isInitial) && autoComplete.showDropDown && autoComplete.options.length > 0;
}
//---------------------------------------------------------
export function isAutoShowError(autoComplete: IAutoComplete): boolean {
   // return !this.isLoading.autoComplete && autoComplete.showDropDown && autoComplete.options.length === 0;
    return !autoComplete.isLoading && autoComplete.showDropDown && autoComplete.options.length === 0;
}
//---------------------------------------------------------
export function onAutoFocus(autoComplete: IAutoComplete) {
    (autoComplete.name || '').length >= 2 ? autoComplete.showDropDown = true : autoComplete.showDropDown = false;
}
//---------------------------------------------------------
export function onAutoInputChange(apiService: HttpService,value: any, autoComplete: IAutoComplete): void {
    const val = value ?? '';
    // Reset selected ID
    initAuto(autoComplete);
    doAuto(apiService, autoComplete, val);
}
//---------------------------------------------------------
export function initAuto(autoComplete: IAutoComplete) {
    autoComplete.name = undefined;
    autoComplete.id = undefined;
    autoComplete.options = [];
    autoComplete.showDropDown = false;
    autoComplete.isInitial = true;
}
//---------------------------------------------------------
export function hideAutoOverlay(autoComplete: IAutoComplete) {
    setTimeout(() => autoComplete.showDropDown = false, 200);
}
//---------------------------------------------------------
