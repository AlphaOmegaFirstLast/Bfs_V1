
import { IEntityRequest, IEntity, IQueryColumn, IAction } from "@bfs/_shared/interfaces";
//------------------------ Operation Business Specific ---------------------------------
import * as operations from '@bfs/infrastructure-main/infrastructure.operations';

import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, FormBuilder } from "@angular/forms";

// Output Columns of a Query  [used in entity Query]
export const DeploymentAzureColumns = [
    { fieldName: 'project', displayName: 'Project', sortName: 'Project', width: '50px', isVisible:false },
{ fieldName: 'id', displayName: 'ID', sortName: 'Id', width: '50px', isVisible:false },
{ fieldName: 'scriptFile', displayName: 'ScriptFile', sortName: 'ScriptFile', width: '50px', isVisible:false },
{ fieldName: 'bfsSystemId', displayName: 'BestFit System', sortName: 'BfsSystem', width: '50px', isVisible:false },
{ fieldName: 'sourceProject', displayName: 'SourceProject', sortName: 'SourceProject', width: '50px', isVisible:false },
{ fieldName: 'sourcePath', displayName: 'SourcePath', sortName: 'SourcePath', width: '50px', isVisible:false },
{ fieldName: 'publishPath', displayName: 'PublishPath', sortName: 'PublishPath', width: '50px', isVisible:false },
{ fieldName: 'config', displayName: 'Config', sortName: 'Config', width: '50px', isVisible:true },
{ fieldName: 'environmentValue', displayName: 'EnvironmentValue', sortName: 'EnvironmentValue', width: '50px', isVisible:true },
{ fieldName: 'targetVirtualDir', displayName: 'TargetVirtualDir', sortName: 'TargetVirtualDir', width: '50px', isVisible:false },
{ fieldName: 'publishProfilePath', displayName: 'PublishProfilePath', sortName: 'PublishProfilePath', width: '50px', isVisible:false },
{ fieldName: 'appService', displayName: 'AppService', sortName: 'AppService', width: '50px', isVisible:true },
{ fieldName: 'resourceGroup', displayName: 'ResourceGroup', sortName: 'ResourceGroup', width: '50px', isVisible:false },

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
targetVirtualDir?: string;
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
    project: [''],
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

links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: 0, route:'/bfs/deployment-azure/add', displayText: 'Add New record'
});
links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/bfs/deployment-azure/view', displayText: 'View...'
});
links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/bfs/deployment-azure/edit', displayText: 'Edit...' 
});
links.push({
actionSource:'System', actionType:'FrontendLink', actionLocation:'ListRow',recordId: record['id'], route:'/bfs/deployment-azure/delete', displayText: 'Delete...' 
});

links.push({
actionSource:'System', actionType:'FrontendFunction', actionLocation:'ListRow',recordId: record['id'], action: operations.deploy, displayText: 'Deploy', data: { recordId: record['id'], putUrl: '/Operations/BfsSystem/Deploy/Azure' }
});
links.push({
actionSource:'System', actionType:'FrontendFunction', actionLocation:'FormHeader',recordId: record['id'], action: operations.deploy, displayText: 'Deploy', data: { recordId: record['id'], putUrl: '/Operations/BfsSystem/Deploy/Azure' }
});

        return links;
    }
    //---------------------------------------------------------

