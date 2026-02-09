
import { IEntityRequest, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
//------------------------ Operation Business Specific ---------------------------------
import { deleteTree, duplicateRecord, duplicateTree } from '@bfs/infrastructure-main/infrastructure.operations';

import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, FormBuilder } from "@angular/forms";

// Output Columns of a Query  [used in entity Query]
export const DeploymentAzureColumns = [
    { fieldName: 'deploymentAzureProject', displayName: 'Project', sortName: 'Project', width: '50px', isVisible:true },
{ fieldName: 'deploymentAzureId', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:true },
{ fieldName: 'deploymentAzureScriptFile', displayName: 'ScriptFile', sortName: 'ScriptFile', width: '50px', isVisible:true },
{ fieldName: 'deploymentAzureBfsSystemId', displayName: 'System Info', sortName: 'BfsSystem', width: '50px', isVisible:true },
{ fieldName: 'deploymentAzureSourceProject', displayName: 'SourceProject', sortName: 'SourceProject', width: '50px', isVisible:true },
{ fieldName: 'deploymentAzureSourcePath', displayName: 'SourcePath', sortName: 'SourcePath', width: '50px', isVisible:true },
{ fieldName: 'deploymentAzurePublishPath', displayName: 'PublishPath', sortName: 'PublishPath', width: '50px', isVisible:true },
{ fieldName: 'deploymentAzureConfig', displayName: 'Config', sortName: 'Config', width: '50px', isVisible:true },
{ fieldName: 'deploymentAzureEnvironmentValue', displayName: 'EnvironmentValue', sortName: 'EnvironmentValue', width: '50px', isVisible:true },
{ fieldName: 'deploymentAzureTargetVirtualFolder', displayName: 'TargetVirtualFolder', sortName: 'TargetVirtualFolder', width: '50px', isVisible:true },
{ fieldName: 'deploymentAzurePublishProfilePath', displayName: 'PublishProfilePath', sortName: 'PublishProfilePath', width: '50px', isVisible:true },
{ fieldName: 'deploymentAzureAppService', displayName: 'AppService', sortName: 'AppService', width: '50px', isVisible:true },
{ fieldName: 'deploymentAzureResourceGroup', displayName: 'ResourceGroup', sortName: 'ResourceGroup', width: '50px', isVisible:true },

];
//---------------------------------------------------------
export interface IDeploymentAzure {
    project?: string;
isDeleted?: boolean;
id?: string;
scriptFile?: string;
sourceProject?: string;
sourcePath?: string;
publishPath?: string;
config?: string;
environmentValue?: string;
targetVirtualFolder?: string;
publishProfilePath?: string;
appService?: string;
resourceGroup?: string;

    bfsSystemId?: string;

}
//---------------------------------------------------------
export function initDeploymentAzure(): IDeploymentAzure {
    let entity: IDeploymentAzure = {
        project: '',
isDeleted: false,
id: '0',
scriptFile: '',
sourceProject: '',
sourcePath: '',
publishPath: '',
config: '',
environmentValue: '',
targetVirtualFolder: '',
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
    project: [''],
isDeleted: [false],
id: ['0'],
scriptFile: [''],
sourceProject: [''],
sourcePath: [''],
publishPath: [''],
config: [''],
environmentValue: [''],
targetVirtualFolder: [''],
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

export function getDeploymentAzureActions(record: IQueryColumn): IAction[] {
        let links: IAction[] = [];

links.push({
actionSource:'0', actionType:'FrontendLink', actionLocation:'ListHeader',recordId: 0, route:'/bfs/deployment-azure/add', displayText: 'Add New record' 
});
links.push({
actionSource:'0', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['deploymentAzureId'], route:'/bfs/deployment-azure/view', displayText: 'View...' 
});
links.push({
actionSource:'0', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['deploymentAzureId'], route:'/bfs/deployment-azure/edit', displayText: 'Edit...' 
});
links.push({
actionSource:'0', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['deploymentAzureId'], route:'/bfs/deployment-azure/delete', displayText: 'Delete...' 
});
links.push({
actionSource:'0', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['bfsSystemId'], route:'/bfs/bfs-system/view', displayText:'Go to BfsSystem' 
});

links.push({
actionSource:'0', actionType:'FrontendFunction', actionLocation:'ListRow',recordId: record['deploymentAzureId'], action: duplicateRecord, displayText: 'Duplicate Record', data: { postUrl: '/DeploymentAzure', onSuccessMethodName: 'getReport' } 
});
links.push({
actionSource:'0', actionType:'FrontendFunction', actionLocation:'ListRow',recordId: record['deploymentAzureId'], action: duplicateTree, displayText: 'Duplicate Tree', data: { postUrl: '/Operations/DeploymentAzure/DuplicateTree', onSuccessMethodName: 'getReport' } 
});

        return links;
    }
    //---------------------------------------------------------

