import { SspTransactionFormComponent } from "@bfs/stockex-tables/ssp-transaction/ssp-transaction.form.component";
import { IIdentifiable, IUserInterface } from "../_shared/interfaces";
import { ISspTransaction } from "@bfs/stockex-tables/ssp-transaction/ssp-transaction.shared";
import { CashTransactionFormComponent } from "@bfs/stockex-tables/cash-transaction/cash-transaction.form.component";

//---------------------------------------------------------
// I copied code of Base-Form.add function, 
// and I modified it to work specifically with sspTransaction
// and can be called as a business action.
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
// I copied code of Base-Form.add function, 
// and I modified it to work specifically with sspTransaction
// and can be called as a business action.
export async function cashTransactionRollout(me: CashTransactionFormComponent, record: IIdentifiable, data: any) {
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
