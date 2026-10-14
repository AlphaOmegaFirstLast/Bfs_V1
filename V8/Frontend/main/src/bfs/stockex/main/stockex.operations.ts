//Template_Start_Code_DontOverwrite_1
import { IIdentifiable } from "@bfs/_shared/interfaces";
import { ISspTransaction } from "@bfs/stockex/tables/ssp-transaction/ssp-transaction.shared";
import { SspTransactionFormComponent } from "@bfs/stockex/tables/ssp-transaction/ssp-transaction.form.component";
import { CashTransactionFormComponent } from "@bfs/stockex/tables/cash-transaction/cash-transaction.form.component";
import { ICashTransaction } from "../tables/cash-transaction/cash-transaction.shared";

//---------------------------------------------------------
//it is called as a business action.
export async function sspTransactionRollout(me: SspTransactionFormComponent, record: IIdentifiable, data: any) {
    // implement me.validSubmit();
    me.submit = true
    if (me.validationForm.valid) {
        // One-time sync to entity object
        me.entity = me.validationForm.getRawValue();
        me.messages = [];
        if (!me.isLoading.save) {  // to prevent multiple requests

            // implement me.applyOperation();
            var target = data.postUrl;  // postUrl of sspTransactionRollout.
            me.isLoading.save = true;

            (await me.apiService.post(target, me.entity)).subscribe({
                next: (response: ISspTransaction) => {
                    me.isLoading.save = false;
                    me.submit = false;
                    me.validationForm.patchValue(me.entity);
                    me.messages.push({ text: `${me.entityDisplayName} was added successfully`, msgType: "info" });
                },
                error: (err: any) => {
                    me.isLoading.save = false;
                    var msg = err.message || `An error occurred while adding ${me.entityDisplayName} data.`;
                    me.messages.push({ text: msg, msgType: "danger" });
                }
            });
        }
    }
}
//---------------------------------------------------------
//it is called as a business action.
export async function cashTransactionRollout(me: CashTransactionFormComponent, record: IIdentifiable, data: any) {
    // implement me.validSubmit();
    me.submit = true
    if (me.validationForm.valid) {
        // One-time sync to entity object
        me.entity = me.validationForm.getRawValue();
        me.messages = [];
        if (!me.isLoading.save) {  // to prevent multiple requests

            // implement me.applyOperation();
            var target = data.postUrl;  // postUrl of cashTransactionRollout.
            me.isLoading.save = true;

            (await me.apiService.post(target, me.entity)).subscribe({
                next: (response: ICashTransaction) => {
                    me.isLoading.save = false;
                    me.submit = false;
                    me.validationForm.patchValue(me.entity);
                    me.messages.push({ text: `${me.entityDisplayName} was added successfully`, msgType: "info" });
                },
                error: (err: any) => {
                    me.isLoading.save = false;
                    var msg = err.message || `An error occurred while adding ${me.entityDisplayName} data.`;
                    me.messages.push({ text: msg, msgType: "danger" });
                }
            });
        }
    }
}
//---------------------------------------------------------
//it is called as a business action.
export async function sspTransactionRollback(me: SspTransactionFormComponent, record: IIdentifiable, data: any) {
    // implement me.validSubmit();
    me.submit = true
    if (me.validationForm.valid) {
        // One-time sync to entity object
        me.entity = me.validationForm.getRawValue();
        me.messages = [];
        if (!me.isLoading.save) {  // to prevent multiple requests

            // implement me.applyOperation();
            var target = data.deleteUrl+'/' + me.entity.id;  // deleteUrl of sspTransactionRollback.
            me.isLoading.save = true;

            (await me.apiService.delete(target)).subscribe({
                next: (response: ISspTransaction) => {
                    me.isLoading.save = false;
                    me.submit = false;
                    me.validationForm.patchValue(me.entity);
                    me.messages.push({ text: `${me.entityDisplayName} was deleted successfully`, msgType: "info" });
                },
                error: (err: any) => {
                    me.isLoading.save = false;
                    var msg = err.message || `An error occurred while deleting ${me.entityDisplayName} data.`;
                    me.messages.push({ text: msg, msgType: "danger" });
                }
            });
        }
    }
}
//---------------------------------------------------------
//it is called as a business action.
export async function cashTransactionRollback(me: CashTransactionFormComponent, record: IIdentifiable, data: any) {
    // implement me.validSubmit();
    me.submit = true
    if (me.validationForm.valid) {
        // One-time sync to entity object
        me.entity = me.validationForm.getRawValue();
        me.messages = [];
        if (!me.isLoading.save) {  // to prevent multiple requests

            // implement me.applyOperation();
            var target = data.deleteUrl+'/' + me.entity.id;  // deleteUrl of cashTransactionRollback.
            me.isLoading.save = true;

            (await me.apiService.delete(target)).subscribe({
                next: (response: ICashTransaction) => {
                    me.isLoading.save = false;
                    me.submit = false;
                    me.validationForm.patchValue(me.entity);
                    me.messages.push({ text: `${me.entityDisplayName} was deleted successfully`, msgType: "info" });
                },
                error: (err: any) => {
                    me.isLoading.save = false;
                    var msg = err.message || `An error occurred while deleting ${me.entityDisplayName} data.`;
                    me.messages.push({ text: msg, msgType: "danger" });
                }
            });
        }
    }
}
//---------------------------------------------------------
//it is called as a business action.
export async function sspTransactionChange(me: SspTransactionFormComponent, record: IIdentifiable, data: any) {
    // implement me.validSubmit();
    me.submit = true
    if (me.validationForm.valid) {
        // One-time sync to entity object
        me.entity = me.validationForm.getRawValue();
        me.messages = [];
        if (!me.isLoading.save) {  // to prevent multiple requests

            // implement me.applyOperation();
            var target = data.putUrl;  // putUrl of sspTransactionChange.
            me.isLoading.save = true;

            (await me.apiService.put(target, me.entity)).subscribe({
                next: (response: ISspTransaction) => {
                    me.isLoading.save = false;
                    me.submit = false;
                    me.validationForm.patchValue(me.entity);
                    me.messages.push({ text: `${me.entityDisplayName} was updated successfully`, msgType: "info" });
                },
                error: (err: any) => {
                    me.isLoading.save = false;
                    var msg = err.message || `An error occurred while updating ${me.entityDisplayName} data.`;
                    me.messages.push({ text: msg, msgType: "danger" });
                }
            });
        }
    }
}
//---------------------------------------------------------
//it is called as a business action.
export async function cashTransactionChange(me: CashTransactionFormComponent, record: IIdentifiable, data: any) {
    // implement me.validSubmit();
    me.submit = true
    if (me.validationForm.valid) {
        // One-time sync to entity object
        me.entity = me.validationForm.getRawValue();
        me.messages = [];
        if (!me.isLoading.save) {  // to prevent multiple requests

            // implement me.applyOperation();
            var target = data.putUrl;  // putUrl of cashTransactionChange.
            me.isLoading.save = true;

            (await me.apiService.put(target, me.entity)).subscribe({
                next: (response: ICashTransaction) => {
                    me.isLoading.save = false;
                    me.submit = false;
                    me.validationForm.patchValue(me.entity);
                    me.messages.push({ text: `${me.entityDisplayName} was updated successfully`, msgType: "info" });
                },
                error: (err: any) => {
                    me.isLoading.save = false;
                    var msg = err.message || `An error occurred while updating ${me.entityDisplayName} data.`;
                    me.messages.push({ text: msg, msgType: "danger" });
                }
            });
        }
    }
}
//---------------------------------------------------------
//Template_End_Code_DontOverwrite_1
