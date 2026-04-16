
import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
//------------------------ Operation Business Specific ---------------------------------
import * as operations from '@bfs/master-main/master.operations';

import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, FormBuilder } from "@angular/forms";

// Output Columns of a Query  [used in entity Query]
export const DeploymentAzureColumns = [
    { fieldName: 'id', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:false },
{ fieldName: 'scriptFile', displayName: 'ScriptFile', sortName: 'ScriptFileName', width: '50px', isVisible:false },
{ fieldName: 'bfsSystemId', displayName: 'BestFit System', sortName: 'BfsSystemName', width: '50px', isVisible:false },
{ fieldName: 'sourceProject', displayName: 'SourceProject', sortName: 'SourceProjectName', width: '50px', isVisible:false },
{ fieldName: 'sourcePath', displayName: 'SourcePath', sortName: 'SourcePathName', width: '50px', isVisible:false },
{ fieldName: 'publishPath', displayName: 'PublishPath', sortName: 'PublishPathName', width: '50px', isVisible:false },
{ fieldName: 'config', displayName: 'Config', sortName: 'ConfigName', width: '50px', isVisible:true },
{ fieldName: 'environmentValue', displayName: 'EnvironmentValue', sortName: 'EnvironmentValueName', width: '50px', isVisible:true },
{ fieldName: 'targetVirtualDir', displayName: 'TargetVirtualDir', sortName: 'TargetVirtualDirName', width: '50px', isVisible:false },
{ fieldName: 'publishProfilePath', displayName: 'PublishProfilePath', sortName: 'PublishProfilePathName', width: '50px', isVisible:false },
{ fieldName: 'appService', displayName: 'AppService', sortName: 'AppServiceName', width: '50px', isVisible:true },
{ fieldName: 'resourceGroup', displayName: 'ResourceGroup', sortName: 'ResourceGroupName', width: '50px', isVisible:false },

];
//---------------------------------------------------------
export interface IDeploymentAzure {
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
    isDeleted: [false],
id: ['0'],
scriptFile: [''],
sourceProject: [''],
sourcePath: [''],
publishPath: [''],
config: [''],
environmentValue: [''],
targetVirtualDir: [''],
publishProfilePath: [''],
appService: [''],
resourceGroup: [''],

    bfsSystemId: ['0'],

    };
} 
//---------------------------------------------------------
export interface IDeploymentAzureWithLookup extends IDeploymentAzure{

    bfsSystemName?: string;

}
//---------------------------------------------------------
export interface IDeploymentAzureRequest extends IEntityRequest<IDeploymentAzureFilter> {}

//---------------------------------------------------------
export interface IDeploymentAzureFilter {
    [key: string]: any;

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

            BfsSystemId: undefined ,

            }
    };

    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------

export function getDeploymentAzureActions(component: any, record: IEntity): IAction[] {
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

