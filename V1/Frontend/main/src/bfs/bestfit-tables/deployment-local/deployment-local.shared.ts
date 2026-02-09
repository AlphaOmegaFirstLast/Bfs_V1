import { IEntityRequest } from "@bfs/_shared/interfaces";

import { UntypedFormGroup, Validators, AbstractControl, ValidatorFn, FormBuilder } from "@angular/forms";

// Output Columns of a Query  [used in entity Query]
export const DeploymentLocalColumns = [
    { fieldName: 'isDeleted', displayName: 'IsDeleted', sortName:'IsDeleted', width: '50px', isVisible:false },
{ fieldName: 'id', displayName: 'ID', sortName:'Id', width: '50px', isVisible:false },
{ fieldName: 'scriptFile', displayName: 'ScriptFile', sortName:'ScriptFile', width: '50px', isVisible:true },
{ fieldName: 'sourceProject', displayName: 'SourceProject', sortName:'SourceProject', width: '50px', isVisible:true },
{ fieldName: 'sourcePath', displayName: 'SourcePath', sortName:'SourcePath', width: '50px', isVisible:true },
{ fieldName: 'publishPath', displayName: 'PublishPath', sortName:'PublishPath', width: '50px', isVisible:true },
{ fieldName: 'config', displayName: 'Config', sortName:'Config', width: '50px', isVisible:true },
{ fieldName: 'environmentValue', displayName: 'EnvironmentValue', sortName:'EnvironmentValue', width: '50px', isVisible:true },
{ fieldName: 'targetVirtualFolder', displayName: 'TargetVirtualFolder', sortName:'TargetVirtualFolder', width: '50px', isVisible:true },
{ fieldName: 'webSite', displayName: 'WebSite', sortName:'WebSite', width: '50px', isVisible:true },
{ fieldName: 'appPoolName', displayName: 'AppPoolName', sortName:'AppPoolName', width: '50px', isVisible:true },
{ fieldName: 'port', displayName: 'Port', sortName:'Port', width: '50px', isVisible:true },
{ fieldName: 'httpsRequired', displayName: 'isHttpsRequired', sortName:'HttpsRequired', width: '50px', isVisible:true },
{ fieldName: 'project', displayName: 'Project', sortName:'Project', width: '50px', isVisible:true },

    { fieldName: 'systemInfoId', displayName: 'System Info', sortName:'SystemInfo', width: '50px', isVisible:false },

];
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

    systemInfoId: ['0'],

    };
} 
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

    systemInfoId?: string;

}
//---------------------------------------------------------
export interface IDeploymentLocalWithLookup extends IDeploymentLocal{

    systemInfo?: string;

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

        systemInfoId: '0',

    };
    return JSON.parse(JSON.stringify(entity));
}
//---------------------------------------------------------
export interface IDeploymentLocalRequest extends IEntityRequest<IDeploymentLocalFilter> {}

//---------------------------------------------------------
export interface IDeploymentLocalFilter {
    [key: string]: any;

    SystemInfoId?: string;

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

            SystemInfoId: undefined ,

            }
    };

    return JSON.parse(JSON.stringify(request));
}
//---------------------------------------------------------

