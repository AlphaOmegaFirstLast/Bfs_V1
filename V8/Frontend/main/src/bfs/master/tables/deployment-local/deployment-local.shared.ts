import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, ValidationErrors, FormBuilder } from "@angular/forms";

import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
import { getFormControlValidation } from "@bfs/_shared/objectFields";
//------------------------ Operation Business Specific ---------------------------------
import * as operations from '@bfs/master/main/master.operations';
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

// Output Columns of a Query  [used in entity Query]
export const DeploymentLocalColumns = [
    { fieldName: 'id', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:false, columnOrder:1 },
{ fieldName: 'scriptFile', displayName: 'ScriptFile', sortName: 'ScriptFile', width: '50px', isVisible:true, columnOrder:1 },
{ fieldName: 'bfsSystemId', displayName: 'System Info', sortName: 'BfsSystem_Name', width: '50px', isVisible:true, columnOrder:1 },
{ fieldName: 'sourceProject', displayName: 'SourceProject', sortName: 'SourceProject', width: '50px', isVisible:true, columnOrder:1 },
{ fieldName: 'sourcePath', displayName: 'SourcePath', sortName: 'SourcePath', width: '50px', isVisible:false, columnOrder:1 },
{ fieldName: 'publishPath', displayName: 'PublishPath', sortName: 'PublishPath', width: '50px', isVisible:false, columnOrder:1 },
{ fieldName: 'config', displayName: 'Config', sortName: 'Config', width: '50px', isVisible:true, columnOrder:1 },
{ fieldName: 'environmentValue', displayName: 'EnvironmentValue', sortName: 'EnvironmentValue', width: '50px', isVisible:true, columnOrder:1 },
{ fieldName: 'targetVirtualDir', displayName: 'TargetVirtualDir', sortName: 'TargetVirtualDir', width: '50px', isVisible:true, columnOrder:1 },
{ fieldName: 'webSite', displayName: 'WebSite', sortName: 'WebSite', width: '50px', isVisible:true, columnOrder:1 },
{ fieldName: 'appPoolName', displayName: 'AppPoolName', sortName: 'AppPoolName', width: '50px', isVisible:true, columnOrder:1 },
{ fieldName: 'port', displayName: 'Port', sortName: 'Port', width: '50px', isVisible:true, columnOrder:1 },
{ fieldName: 'isHttpsRequired', displayName: 'IsHttpsRequired', sortName: 'IsHttpsRequired', width: '50px', isVisible:true, columnOrder:1 },

];
//---------------------------------------------------------
export interface IDeploymentLocal extends IEntity{
    isDeleted?: boolean;
id?: string;
scriptFile?: string;
sourceProject?: string;
sourcePath?: string;
publishPath?: string;
config?: string;
environmentValue?: string;
targetVirtualDir?: string;
webSite?: string;
appPoolName?: string;
port?: string;
isHttpsRequired?: boolean;

    bfsSystemId?: string;

}
//---------------------------------------------------------
export function initDeploymentLocal(): IDeploymentLocal {
    let entity: IDeploymentLocal = {
        isDeleted: false,
id: '0',
scriptFile: '',
sourceProject: '',
sourcePath: '',
publishPath: '',
config: '',
environmentValue: '',
targetVirtualDir: '',
webSite: '',
appPoolName: '',
port: '',
isHttpsRequired: false,

        bfsSystemId: '0',

    };
    return JSON.parse(JSON.stringify(entity));
}
//---------------------------------------------------------

// Fields of an Entity [used in Entity form]
export function deploymentLocalUntypedFormGroup(formBuilder: FormBuilder): any {
    return {
    isDeleted: [false,getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
id: ['0',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
scriptFile: ['',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"1000","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
sourceProject: ['',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"1000","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
sourcePath: ['',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"1000","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
publishPath: ['',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"1000","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
config: ['',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"1000","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
environmentValue: ['',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"1000","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
targetVirtualDir: ['',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"1000","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
webSite: ['',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"1000","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
appPoolName: ['',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"1000","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
port: ['',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"1000","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
isHttpsRequired: [false,getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],

    bfsSystemId: ['0',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"1000","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],

//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2

    };
} 
//---------------------------------------------------------
export interface IDeploymentLocalRequest extends IEntityRequest<IDeploymentLocalFilter> {}

//---------------------------------------------------------
export interface IDeploymentLocalFilter {
    [key: string]: any;
    Id?: string;

    BfsSystemId?: string;

}
//---------------------------------------------------------
export function initDeploymentLocalRequest(): IDeploymentLocalRequest {
    let request: IDeploymentLocalRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: DeploymentLocalColumns.map(column => ({ ...column })),
        group: '',
        sortOption: {
            sortBy: 'id',
            direction: 'asc'
            },
        filter: {
            Id: undefined ,

            BfsSystemId: undefined ,

            }
    };

    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------
export function renderDeploymentLocal(record: IDeploymentLocal, column: IQueryColumn): any {
        const value = record[column.fieldName as keyof IDeploymentLocal];
        switch (column.fieldName) {
            case 'bfsSystemId':
                return record['bfsSystemName']?.toString();

            default:
                return value;
        }
        return value;
    }
    //---------------------------------------------------------
export function getDeploymentLocalActions(component: any, record: IDeploymentLocal): IAction[] {
        let links: IAction[] = [];

if (component.accessService.isActionAllowed('deploymentLocal', ''))
{links.push({
actionSource:'System', actionType:'FrontendFunction', actionLocation:'ListRow',recordId: record['id'], action: operations.publish, displayText: 'Publish', data: { recordId: record['id'], putUrl: '/Operations/BfsSystem/Publish/Local' }
});
}
if (component.accessService.isActionAllowed('deploymentLocal', ''))
{links.push({
actionSource:'System', actionType:'FrontendFunction', actionLocation:'FormHeader',recordId: record['id'], action: operations.publish, displayText: 'Publish', data: { recordId: record['id'], putUrl: '/Operations/BfsSystem/Publish/Local' }
});
}
if (component.accessService.isActionAllowed('deploymentLocal', ''))
{links.push({
actionSource:'System', actionType:'FrontendFunction', actionLocation:'ListRow',recordId: record['id'], action: operations.deploy, displayText: 'Deploy Local', data: { recordId: record['id'], putUrl: '/Operations/BfsSystem/Deploy/Local' }
});
}
if (component.accessService.isActionAllowed('deploymentLocal', ''))
{links.push({
actionSource:'System', actionType:'FrontendFunction', actionLocation:'FormHeader',recordId: record['id'], action: operations.deploy, displayText: 'Deploy Local', data: { recordId: record['id'], putUrl: '/Operations/BfsSystem/Deploy/Local' }
});
}

        return links;
    }
//---------------------------------------------------------
export function isVisible(entity: IDeploymentLocal, validationForm: UntypedFormGroup, fieldName: string): boolean {
    //check using const control = this.validationForm.get(fieldName);
    // or this.entity[fieldName]
    switch (fieldName) {
        case 'exampleId':
           //example=>  return (entity['transactionTypeId'] == 2 || entity['transactionTypeId'] == 3) ? true : false;
           return true;
    }
    return true;
}
//---------------------------------------------------------
export function isEnabled(entity: IEntity, validationForm: UntypedFormGroup, fieldName: string): boolean {
    //check using const control = this.validationForm.get(fieldName);
    // or this.entity[fieldName]
    switch (fieldName) {
        case 'exampleId':
           //example=>  return (entity['transactionTypeId'] == 2 || entity['transactionTypeId'] == 3) ? true : false;
           return true;
    }
    return true;
}

//Template_Start_Code_DontOverwrite_3
//Template_End_Code_DontOverwrite_3

