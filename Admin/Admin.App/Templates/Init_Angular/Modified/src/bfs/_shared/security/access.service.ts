import { inject, Injectable } from '@angular/core';
import { forkJoin, Observable, of, tap } from 'rxjs';
import { environment } from '@/environment/environment';
import { safeHtmlDecode } from '../helpers/html.helper';
import { AuthService } from '@bfs/auth-main/auth.service';
import { InfrastructureService } from '@bfs/infrastructure-main/infrastructure.service';
import { IQueryResponse, TokenModel, TokenParsed } from '../interfaces';
import { promises } from 'dns';

@Injectable({ providedIn: 'root' })  // Ensure the service is a singleton and available application-wide
export class AccessService {
    authService: AuthService;
    bfsService: InfrastructureService;

    tokenModel: TokenModel | null = null;
    tokenParsed: TokenParsed | null = null;
    isLoading: boolean = false;
    isLoaded: boolean = false;
    bfsAdminRoleId = '1'; // assuming 1 is the roleId for bfs.admin, this value should be consistent with the data in the database

    // Get all components and actions from bfsService.
    public components: any[] = [];
    public systemActions: any[] = [];

    // Get all roles, apps, role-app mapping, role-component-action mapping from authService.
    public roles: any[] = [];
    public apps: any[] = [];
    public roleApp: any[] = [];
    public rolesComponentSystemActions: any[] = [];
    public rolesBusinessActions: any[] = [];

    //-----------------------------------------------------------------  
    constructor() {
        this.authService = inject(AuthService);
        this.bfsService = inject(InfrastructureService);
    }
    //----------------------------------------------------------------- 
    async loadRoleData(): Promise<void> {
        if (this.isLoaded){
            return; // the data is already fetched and cached, no need to load again
        }

        if (this.isLoading) {
            return; // the data is still being loaded, Prevent multiple simultaneous loads
        }

        this.isLoading = true;
        const [components, systemActions, roles, apps, roleApp, roleComponentSystemAction] = await Promise.all([
            this.bfsService.getItems<IQueryResponse>("/bfsComponent/list", { pageSize: 300 }),
            this.bfsService.getItems<IQueryResponse>("/SystemAction/list", { pageSize: 300 }),

            this.authService.getItems<IQueryResponse>("/AuthRole/list", { pageSize: 300 }),
            this.authService.getItems<IQueryResponse>("/AuthApp/list", { pageSize: 300 }),
            this.authService.getItems<IQueryResponse>("/AuthRoleApp/list", { pageSize: 300 }),
            this.authService.getItems<IQueryResponse>("/AuthRoleComponentSystemAction/list", { pageSize: 300 })

        ]);
        //after you get the data, cache it in the service properties so that it can be used for subsequent calls without needing to fetch from the server again
        this.isLoading = false;
        this.isLoaded = true;
        this.components = components.items;
        this.systemActions = systemActions.items;
        this.roles = roles.items;
        this.apps = apps.items;
        this.roleApp = roleApp.items;
        this.rolesComponentSystemActions = roleComponentSystemAction.items;
        //  this.rolesBusinessActions = rolesBusinessActions.items; 
    }
    //-----------------------------------------------------------------   
    async IsAccessServiceReady(): Promise<boolean> {
        if (!this.isLoaded && !this.isLoading ) {  // if not loaading means either the data is already loaded or not started loading yet, in both cases we want to attempt to load the data if it is not loaded yet
            await this.loadRoleData();
        }
        // this sentance will not guarantee the data is loaded as loadRoleData is async and we are not awaiting it in this method, 
        // but it will ensure that the loading process is triggered if it has not been triggered yet, 
        // and it will return true if the data is already loaded or loading is completed, false if the loading is still in progress.
        return this.isLoaded && !this.isLoading;
    }
    //-----------------------------------------------------------------
    // to handle a special case when an array has only one value, it is passed as a string.
    ensureArray(value: null | undefined | string | string[]): string[] {
        var result: string[] = [];
        if (!(value === null || value === undefined))
            result = Array.isArray(value) ? value : [value];

        return result.length > 0 ? result.map(r => String(r).toLowerCase()) : [];
    }
    //------------------------------------------------------------
    async isActionAllowed(component: string, action: string): Promise<boolean> {

        return true ;
        if (environment.isSecurityEnabled === false) {
            return true; // Allow access if security is disabled
        }
        let permissions = [
            { action: "view", component: "StrStore" },
            { action: "edit", component: "StrStore" },
            { action: "add", component: "StrProduct" },
            { action: "view", component: "StrProduct" },
            // {action:"edit", component:"StrProduct"},
            // {action:"delete", component:"StrProduct"},
        ];

        let userRoles = await this.getUserRoles() || [];
        return (
            userRoles.some(role => role.toLowerCase() === 'bfs.admin') || // BfsAdmin has access to everything
            (
                permissions.some(x => x.component.toLowerCase() === component.toLowerCase()
                    && x.action.toLowerCase() === action.toLowerCase())
            )
        ); // Check if the user has access to the action/method
    }
    //------------------------------------------------------------

    public async isAccessible(data: any): Promise<boolean> {
        if (environment.isSecurityEnabled === false || this.isBfsAdmin()) { // BfsAdmin has access to everything
            return true; // Allow access if security is disabled
        }

        // if not bfsAdminRoleId, then run the normal logic.
        // get Roles, Apps, Methods Names from the Api.
        // use the names to compare with the required Roles, Apps, Methods defined here. 
        let dataReady = await this.IsAccessServiceReady();
        if (dataReady) {
            // Get the required permissions by the requester
            let requiredRoles = this.ensureArray((data['role'] as string[]));
            let requiredApps = this.ensureArray((data['app'] as string[]));
            let requiredMethods = this.ensureArray((data['method'] as string[]));

            // Get the current user's permissions from token service
            let userRoles = this.ensureArray(await this.getUserRoles() as string[]);
            let userApps = this.ensureArray(await this.getUserApps() as string[]);
            let userMethods = this.ensureArray(await this.getUserMethods() as string[]);

            // Check if the user has a required role, api, or app. it is case sensitive, so ensure the values are consistent
            // .some is more efficient than .includes in this case as it stops checking as soon as a match is found
            if ((requiredRoles.length === 0 || requiredRoles.some(role => userRoles.some(userRole => userRole === role))) &&
                (requiredApps.length === 0 || requiredApps.some(app => userApps.some(userApp => userApp === app))) &&
                (requiredMethods.length === 0 || requiredMethods.some(method => userMethods.some(userMethod => userMethod === method)))) {
                return true; // Allow access
            }
        }

        return false;
    }
    //-----------------------------------------------------------------
    private isBfsAdmin(): boolean {
        if (!this.tokenParsed) {
            this.setTokenParsed();
        }

        return this.tokenParsed?.role?.includes(this.bfsAdminRoleId) || false;
    }
    //-----------------------------------------------------------------
    private setTokenParsed(): void {
        this.tokenParsed = this.tokenModel?.tokenParsed || JSON.parse(safeHtmlDecode(sessionStorage.getItem('token-parsed')?.toString()) || 'null');
    }
    //------------------------------------------------------------

    private async getUserRoles(): Promise<string[]> {
        if (!this.tokenParsed) {
            this.setTokenParsed();
        }
        let userRoleIds = this.tokenParsed?.role || [];
        let roles = this.roles?.filter(x => userRoleIds.includes(x.id)).map(x => x.name) || [];
        return roles;
    }
    //------------------------------------------------------------

    private async getUserMethods(): Promise<string[]> {
        let methods: string[] = [];
        if (!this.tokenParsed) {
            this.setTokenParsed();
        }

        let userRoleIds = this.tokenParsed?.role || [];
        let componentActionIds = this.rolesComponentSystemActions.filter(ra => userRoleIds.includes(ra.roleId));
        for (let ca of componentActionIds || []) {
            let actionNames = this.systemActions?.find(x => x.id === ca.systemActionId)?.name || [];
            let componentNames = this.components?.find(x => x.id === ca.componentId)?.name || [];
            methods.push(`${actionNames}.${componentNames}`);
        }
        return methods;
    }
    //------------------------------------------------------------
    public async getUserApps(): Promise<string[]> {

        if (this.isBfsAdmin()) {
            return this.apps.map(a => a.name) || [];
        }
        else {
            let userRoleIds = this.tokenParsed?.role || [];
            let appIds = this.roleApp.filter(ra => userRoleIds.includes(ra.roleId)).map(ra => ra.appId);
            let apps = this.apps.filter(app => appIds?.includes(app.id)).map(a => a.name) || [];
            return apps;
        }
    }
    //------------------------------------------------------------

}
