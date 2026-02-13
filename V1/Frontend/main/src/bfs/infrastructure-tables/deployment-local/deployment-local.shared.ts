
import { IEntityRequest, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
//------------------------ Operation Business Specific ---------------------------------
import { deleteTree, duplicateRecord, duplicateTree } from '@bfs/infrastructure-main/infrastructure.operations';

import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, FormBuilder } from "@angular/forms";

// Output Columns of a Query  [used in entity Query]
export const DeploymentLocalColumns = [
    { fieldName: 'id', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:true },
{ fieldName: 'scriptFile', displayName: 'ScriptFile', sortName: 'ScriptFile', width: '50px', isVisible:true },
{ fieldName: 'bfsSystemId', displayName: 'System Info', sortName: 'BfsSystem', width: '50px', isVisible:true },
{ fieldName: 'sourceProject', displayName: 'SourceProject', sortName: 'SourceProject', width: '50px', isVisible:true },
{ fieldName: 'sourcePath', displayName: 'SourcePath', sortName: 'SourcePath', width: '50px', isVisible:true },
{ fieldName: 'publishPath', displayName: 'PublishPath', sortName: 'PublishPath', width: '50px', isVisible:true },
{ fieldName: 'config', displayName: 'Config', sortName: 'Config', width: '50px', isVisible:true },
{ fieldName: 'environmentValue', displayName: 'EnvironmentValue', sortName: 'EnvironmentValue', width: '50px', isVisible:true },
{ fieldName: 'targetVirtualFolder', displayName: 'TargetVirtualFolder', sortName: 'TargetVirtualFolder', width: '50px', isVisible:true },
{ fieldName: 'webSite', displayName: 'WebSite', sortName: 'WebSite', width: '50px', isVisible:true },
{ fieldName: 'appPoolName', displayName: 'AppPoolName', sortName: 'AppPoolName', width: '50px', isVisible:true },
{ fieldName: 'port', displayName: 'Port', sortName: 'Port', width: '50px', isVisible:true },
{ fieldName: 'httpsRequired', displayName: 'isHttpsRequired', sortName: 'HttpsRequired', width: '50px', isVisible:true },
{ fieldName: 'project', displayName: 'Project', sortName: 'Project', width: '50px', isVisible:true },

];
//---------------------------------------------------------
export interface IDeploymentLocal {
    isDeleted?: boolean;
id?: string;
scriptFile?: string;
sourceProject?: string;
sourcePath?: string;
publishPath?: string;
config?: string;
environmentValue?: string;
targetVirtualFolder?: string;
webSite?: string;
appPoolName?: string;
port?: string;
httpsRequired?: boolean;
project?: string;

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
targetVirtualFolder: '',
webSite: '',
appPoolName: '',
port: '',
httpsRequired: false,
project: '',

        bfsSystemId: '0',

    };
    return JSON.parse(JSON.stringify(entity));
}
//---------------------------------------------------------

// Fields of an Entity [used in Entity form]
export function deploymentLocalUntypedFormGroup(formBuilder: FormBuilder): any {
    return {
    isDeleted: [false],
id: ['0'],
scriptFile: [''],
sourceProject: [''],
sourcePath: [''],
publishPath: [''],
config: [''],
environmentValue: [''],
targetVirtualFolder: [''],
webSite: [''],
appPoolName: [''],
port: [''],
httpsRequired: [false],
project: [''],

    bfsSystemId: ['0'],

    };
} 
//---------------------------------------------------------
export interface IDeploymentLocalWithLookup extends IDeploymentLocal{

    bfsSystemName?: string;

}
//---------------------------------------------------------
export interface IDeploymentLocalRequest extends IEntityRequest<IDeploymentLocalFilter> {}

//---------------------------------------------------------
export interface IDeploymentLocalFilter {
    [key: string]: any;

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

            BfsSystemId: undefined ,

            }
    };

    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------

export function getDeploymentLocalActions(record: IQueryColumn): IAction[] {
        let links: IAction[] = [];

links.push({
actionSource:'0', actionType:'FrontendLink', actionLocation:'ListHeader',recordId: 0, route:'/bfs/deployment-local/add', displayText: 'Add New record' 
});
links.push({
actionSource:'0', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/bfs/deployment-local/view', displayText: 'View...' 
});
links.push({
actionSource:'0', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/bfs/deployment-local/edit', displayText: 'Edit...' 
});
links.push({
actionSource:'0', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/bfs/deployment-local/delete', displayText: 'Delete...' 
});
links.push({
actionSource:'0', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['bfsSystemId'], route:'/bfs/bfs-system/view', displayText:'Go to BfsSystem' 
});

links.push({
actionSource:'0', actionType:'FrontendFunction', actionLocation:'ListRow',recordId: record['id'], action: duplicateRecord, displayText: 'Duplicate Record', data: {recordId: record['id'], postUrl: '/DeploymentLocal', onSuccessMethodName: 'getReport' } 
});
links.push({
actionSource:'0', actionType:'FrontendFunction', actionLocation:'ListRow',recordId: record['id'], action: duplicateTree, displayText: 'Duplicate Tree', data: { recordId: record['id'], postUrl: '/Operations/DeploymentLocal/DuplicateTree', onSuccessMethodName: 'getReport' } 
});

        return links;
    }
    //---------------------------------------------------------

