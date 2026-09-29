import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, ValidationErrors, FormBuilder } from "@angular/forms";

import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
import { getFormControlValidation } from "@bfs/_shared/objectFields";
//------------------------ Operation Business Specific ---------------------------------
import * as operations from '@bfs/master/main/master.operations';
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

// Output Columns of a Query  [used in entity Query]
export const DeploymentAzureColumns = [
    { fieldName: 'id', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:false, columnOrder:1 },
{ fieldName: 'scriptFile', displayName: 'ScriptFile', sortName: 'ScriptFile', width: '50px', isVisible:false, columnOrder:1 },
{ fieldName: 'bfsSystemId', displayName: 'BestFit System', sortName: 'BfsSystem_Name', width: '50px', isVisible:false, columnOrder:1 },
{ fieldName: 'sourceProject', displayName: 'SourceProject', sortName: 'SourceProject', width: '50px', isVisible:false, columnOrder:1 },
{ fieldName: 'sourcePath', displayName: 'SourcePath', sortName: 'SourcePath', width: '50px', isVisible:false, columnOrder:1 },
{ fieldName: 'publishPath', displayName: 'PublishPath', sortName: 'PublishPath', width: '50px', isVisible:false, columnOrder:1 },
{ fieldName: 'config', displayName: 'Config', sortName: 'Config', width: '50px', isVisible:true, columnOrder:1 },
{ fieldName: 'environmentValue', displayName: 'EnvironmentValue', sortName: 'EnvironmentValue', width: '50px', isVisible:true, columnOrder:1 },
{ fieldName: 'targetVirtualDir', displayName: 'TargetVirtualDir', sortName: 'TargetVirtualDir', width: '50px', isVisible:false, columnOrder:1 },
{ fieldName: 'publishProfilePath', displayName: 'PublishProfilePath', sortName: 'PublishProfilePath', width: '50px', isVisible:false, columnOrder:1 },
{ fieldName: 'appService', displayName: 'AppService', sortName: 'AppService', width: '50px', isVisible:true, columnOrder:1 },
{ fieldName: 'resourceGroup', displayName: 'ResourceGroup', sortName: 'ResourceGroup', width: '50px', isVisible:false, columnOrder:1 },

];
//---------------------------------------------------------
export interface IDeploymentAzure extends IEntity{
    isDeleted?: boolean;
id?: string;
scriptFile?: string;
sourceProject?: string;
sourcePath?: string;
publishPath?: string;
config?: string;
environmentValue?: string;
targetVirtualDir?: string;
publishProfilePath?: string;
appService?: string;
resourceGroup?: string;

    bfsSystemId?: string;

}
//---------------------------------------------------------
export function initDeploymentAzure(): IDeploymentAzure {
    let entity: IDeploymentAzure = {
        isDeleted: false,
id: '0',
scriptFile: '',
sourceProject: '',
sourcePath: '',
publishPath: '',
config: '',
environmentValue: '',
targetVirtualDir: '',
publishProfilePath: '',
appService: '',
resourceGroup: '',

        bfsSystemId: '0',

    };
    return JSON.parse(JSON.stringify(entity));
}
//---------------------------------------------------------

// Fields of an Entity [used in Entity form]
export function deploymentAzureUntypedFormGroup(formBuilder: FormBuilder): any {
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
publishProfilePath: ['',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"1000","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
appService: ['',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"1000","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],
resourceGroup: ['',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"1000","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],

    bfsSystemId: ['0',getFormControlValidation('{"IsRequired":false,"MinLength":null,"MaxLength":"1000","MinValue":"","MaxValue":"","RegexPattern":"","AllowedValues":""}')],

//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2

    };
} 
//---------------------------------------------------------
export interface IDeploymentAzureRequest extends IEntityRequest<IDeploymentAzureFilter> {}

//---------------------------------------------------------
export interface IDeploymentAzureFilter {
    [key: string]: any;
    Id?: string;

    BfsSystemId?: string;

}
//---------------------------------------------------------
export function initDeploymentAzureRequest(): IDeploymentAzureRequest {
    let request: IDeploymentAzureRequest = {
        pageIndex: 1,
        pageSize: 5,
        columns: DeploymentAzureColumns.map(column => ({ ...column })),
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
export function renderDeploymentAzure(record: IDeploymentAzure, column: IQueryColumn): any {
        const value = record[column.fieldName as keyof IDeploymentAzure];
        switch (column.fieldName) {
            case 'bfsSystemId':
                return record['bfsSystemName']?.toString();

            default:
                return value;
        }
        return value;
    }
    //---------------------------------------------------------
export function getDeploymentAzureActions(component: any, record: IDeploymentAzure): IAction[] {
        let links: IAction[] = [];

if (component.accessService.isActionAllowed('deploymentAzure', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: 0, route:'/mstr/deployment-azure/add', displayText: 'Add New record'
});
}
if (component.accessService.isActionAllowed('deploymentAzure', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/mstr/deployment-azure/view', displayText: 'View...'
});
}
if (component.accessService.isActionAllowed('deploymentAzure', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/mstr/deployment-azure/edit', displayText: 'Edit...' 
});
}
if (component.accessService.isActionAllowed('deploymentAzure', ''))
{links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/mstr/deployment-azure/delete', displayText: 'Delete...' 
});
}

if (component.accessService.isActionAllowed('deploymentAzure', ''))
{links.push({
actionSource:'System', actionType:'FrontendFunction', actionLocation:'ListRow',recordId: record['id'], action: operations.publish, displayText: 'Publish', data: { recordId: record['id'], putUrl: '/Operations/BfsSystem/Publish/Local' }
});
}
if (component.accessService.isActionAllowed('deploymentAzure', ''))
{links.push({
actionSource:'System', actionType:'FrontendFunction', actionLocation:'FormHeader',recordId: record['id'], action: operations.publish, displayText: 'Publish', data: { recordId: record['id'], putUrl: '/Operations/BfsSystem/Publish/Local' }
});
}
if (component.accessService.isActionAllowed('deploymentAzure', ''))
{links.push({
actionSource:'System', actionType:'FrontendFunction', actionLocation:'ListRow',recordId: record['id'], action: operations.deploy, displayText: 'Deploy Azure', data: { recordId: record['id'], putUrl: '/Operations/BfsSystem/Deploy/Azure' }
});
}
if (component.accessService.isActionAllowed('deploymentAzure', ''))
{links.push({
actionSource:'System', actionType:'FrontendFunction', actionLocation:'FormHeader',recordId: record['id'], action: operations.deploy, displayText: 'Deploy Azure', data: { recordId: record['id'], putUrl: '/Operations/BfsSystem/Deploy/Azure' }
});
}

        return links;
    }
//---------------------------------------------------------
export function isVisible(entity: IDeploymentAzure, validationForm: UntypedFormGroup, fieldName: string): boolean {
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

