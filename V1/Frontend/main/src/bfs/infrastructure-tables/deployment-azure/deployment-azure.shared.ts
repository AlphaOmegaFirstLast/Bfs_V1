
import { IEntityRequest, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
//------------------------ Operation Business Specific ---------------------------------
import { deleteTree, duplicateRecord, duplicateTree } from '@bfs/infrastructure-main/infrastructure.operations';

import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, FormBuilder } from "@angular/forms";

// Output Columns of a Query  [used in entity Query]
export const DeploymentAzureColumns = [
    { fieldName: 'project', displayName: 'Project', sortName: 'Project', width: '50px', isVisible:true },
{ fieldName: 'id', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:true },
{ fieldName: 'scriptFile', displayName: 'ScriptFile', sortName: 'ScriptFile', width: '50px', isVisible:true },
{ fieldName: 'bfsSystemId', displayName: 'System Info', sortName: 'BfsSystem', width: '50px', isVisible:true },
{ fieldName: 'sourceProject', displayName: 'SourceProject', sortName: 'SourceProject', width: '50px', isVisible:true },
{ fieldName: 'sourcePath', displayName: 'SourcePath', sortName: 'SourcePath', width: '50px', isVisible:true },
{ fieldName: 'publishPath', displayName: 'PublishPath', sortName: 'PublishPath', width: '50px', isVisible:true },
{ fieldName: 'config', displayName: 'Config', sortName: 'Config', width: '50px', isVisible:true },
{ fieldName: 'environmentValue', displayName: 'EnvironmentValue', sortName: 'EnvironmentValue', width: '50px', isVisible:true },
{ fieldName: 'targetVirtualFolder', displayName: 'TargetVirtualFolder', sortName: 'TargetVirtualFolder', width: '50px', isVisible:true },
{ fieldName: 'publishProfilePath', displayName: 'PublishProfilePath', sortName: 'PublishProfilePath', width: '50px', isVisible:true },
{ fieldName: 'appService', displayName: 'AppService', sortName: 'AppService', width: '50px', isVisible:true },
{ fieldName: 'resourceGroup', displayName: 'ResourceGroup', sortName: 'ResourceGroup', width: '50px', isVisible:true },

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
actionSource:'0', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/bfs/deployment-azure/view', displayText: 'View...' 
});
links.push({
actionSource:'0', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/bfs/deployment-azure/edit', displayText: 'Edit...' 
});
links.push({
actionSource:'0', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/bfs/deployment-azure/delete', displayText: 'Delete...' 
});
links.push({
actionSource:'0', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['bfsSystemId'], route:'/bfs/bfs-system/view', displayText:'Go to BfsSystem' 
});

links.push({
actionSource:'0', actionType:'FrontendFunction', actionLocation:'ListRow',recordId: record['id'], action: duplicateRecord, displayText: 'Duplicate Record', data: {recordId: record['id'], postUrl: '/DeploymentAzure', onSuccessMethodName: 'getReport' } 
});
links.push({
actionSource:'0', actionType:'FrontendFunction', actionLocation:'ListRow',recordId: record['id'], action: duplicateTree, displayText: 'Duplicate Tree', data: { recordId: record['id'], postUrl: '/Operations/DeploymentAzure/DuplicateTree', onSuccessMethodName: 'getReport' } 
});

        return links;
    }
    //---------------------------------------------------------

