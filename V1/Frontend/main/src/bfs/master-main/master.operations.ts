import { IIdentifiable, IUserInterface } from "../_shared/interfaces";

//---------------------------------------------------------

export async function duplicateTree(me: IUserInterface, record: IIdentifiable, data: any) {
    var target = data.postUrl;  // postUrl for tree duplication.
    (await me.apiService.post(target, `${record.id}`)).subscribe({
        next: (res: any) => {
            me.messages.push({ text: 'Record tree duplicated successfully.', msgType: "info" });
            me["getReport"]();
        },
        error: (err: any) => {
            var msg = err.message || 'An error occurred while duplicating the record.';
            me.messages.push({ text: msg, msgType: "danger" });
        }
    });
}
//---------------------------------------------------------

export async function deleteTree(me: IUserInterface, record: IIdentifiable, data: any) {

    var target = data.deleteUrl;
    (await me.apiService.delete(`${target}/${record.id}`)).subscribe({
        next: (res: any) => {
            me.messages.push({ text: 'Record tree deleted successfully.', msgType: "info" });
            me["getReport"]();
        },
        error: (err: any) => {
            var msg = err.message || 'An error occurred while deleting the record.';
            me.messages.push({ text: msg, msgType: "danger" });
        }
    });
}
//---------------------------------------------------------

export async function duplicateRecord(me: IUserInterface, record: IIdentifiable, data: any): Promise<void> {

    if (!me.isLoading.save) {  // to prevent multiple requests
        me.messages = [];
        me.isLoading.save = true;
        var target = `${data.postUrl}/${data.recordId}`;
        (await me.apiService.get(target)).subscribe({
            next: async (res: any) => {
                me.isLoading.list = false;
                var duplicatedRecord = res;
                data = { ...data, record: duplicatedRecord };
                await postDuplicateRecord(me, duplicatedRecord, data);
            },
            error: (err: any) => {
                me.isLoading.save = false;
                var msg = err.message || 'An error occurred while processing Systems data.';
                me.messages.push({ text: msg, msgType: "danger" });
            }
        });
    }
}
//---------------------------------------------------------

export async function postDuplicateRecord(me: IUserInterface, record: any, data: any): Promise<void> {
    if (record as IIdentifiable) {

        record.id = 0; // reset id only for record duplication 

        var target = data.postUrl;  // for record duplication the default postUrl is used, for tree duplication a different url is used
        (await me.apiService.post(target, record)).subscribe({
            next: (res: any) => {
                me.messages.push({ text: 'Record duplicated successfully.', msgType: "info" });
                if (data.onSuccessMethodName)
                    me[data.onSuccessMethodName]();
            },
            error: (err: any) => {
                var msg = err.message || 'An error occurred while duplicating the record.';
                me.messages.push({ text: msg, msgType: "danger" });
            }
        });
    }
    else {
        var msg = 'Record does not implement IIdentifiable interface. Cannot reset id for duplication.';
        me.messages.push({ text: msg, msgType: "danger" });
    }
}
//---------------------------------------------------------

export async function deploy(me: IUserInterface, record: IIdentifiable, data: any) {
    var target = data.putUrl;
    (await me.apiService.put(`${target}/${record.id}`)).subscribe({
        next: (res: any) => {
            me.messages.push({ text: 'Deployment is a success.', msgType: "info" });
        },
        error: (err: any) => {
            var msg = err.message || 'An error occurred while deploying process.';
            me.messages.push({ text: msg, msgType: "danger" });
        }
    });
}
//--------------------------------------------------------

export async function publish(me: IUserInterface, record: IIdentifiable, data: any) {
    var target = data.putUrl;
    (await me.apiService.put(`${target}/${record.id}`)).subscribe({
        next: (res: any) => {
            me.messages.push({ text: 'Publish is a success.', msgType: "info" });
        },
        error: (err: any) => {
            var msg = err.message || 'An error occurred while publishing process.';
            me.messages.push({ text: msg, msgType: "danger" });
        }
    });
}
//--------------------------------------------------------

export async function generateTestData(me: IUserInterface, record: IIdentifiable, data: any) {
    var target = data.getUrl;
    (await me.apiService.put(`${target}/${record.id}`)).subscribe({
        next: (res: any) => {
            me.messages.push({ text: 'Generate Test Data is a success.', msgType: "info" });
        },
        error: (err: any) => {
            var msg = err.message || 'An error occurred while generating test data.';
            me.messages.push({ text: msg, msgType: "danger" });
        }
    });
}
//--------------------------------------------------------
export async function setComponentDefaultActions(me: any, record: IIdentifiable, data: any): Promise<void> {
    let entityId = record.id;
    if (!me.isLoading.save) {  // to prevent multiple requests
        me.messages = [];
        me.isLoading.save = true;
        var target = `/Operations/BfsComponentSystemAction/matrix/${entityId}`;

        var childrenList: any[] = [
            { "bfsComponentId": entityId, "actionLocationId": "1", "systemActionId": "1" }
            , { "bfsComponentId": entityId, "actionLocationId": "2", "systemActionId": "2" }
            , { "bfsComponentId": entityId, "actionLocationId": "2", "systemActionId": "3" }
            , { "bfsComponentId": entityId, "actionLocationId": "2", "systemActionId": "4" }
            , { "bfsComponentId": entityId, "actionLocationId": "2", "systemActionId": "5" }
        ];

        (await me.apiService.put(target, childrenList)).subscribe({
            next: (res: any) => {
                me.isLoading.save = false;
                me.list = res.items;
            },
            error: (err: any) => {
                me.isLoading.save = false;
                var msg = err.message || 'An error occurred while saving matrix data.';
                me.messages.push({ text: msg, msgType: "danger" });
            }
        });
    }
}
//---------------------------------------------------------
