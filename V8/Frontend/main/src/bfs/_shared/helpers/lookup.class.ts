import { ILookup, IQueryResponse } from "../interfaces";
import { HttpService } from '../services/http.service';

export interface ILookupHelper {
    apiService: HttpService,
    queryUrl: string,
    options?: ILookup[],
    fieldName?: string,
    filter?: any;
    pageSize?: any
}

export class LookupHelper {
    apiService!: HttpService;
    fieldName?: string = '';

    queryUrl: string = '';
    options?: ILookup[] = [];
    filter?: any;
    pageSize?: any = 50;

    constructor(lookup: Partial<LookupHelper>) {
        Object.assign(this, lookup);
    }
    //------------------------------------------------------

    // Returns a function that performs the async call when executed inside Promise.all
    // Returns a Promise directly and executes immediately when called
    fetch(): Promise<ILookup[]> {
        if (!this.apiService) {
            throw new Error(`apiService is not set for field: ${this.fieldName}`);
        }

        return this.apiService.getItems<IQueryResponse>(this.queryUrl, { "filter": this.filter, pageSize: this.pageSize })
            .then((response: IQueryResponse) => {
                let items = response?.items?.map((item: any) => ({ id: item.id, name: item.lookupName || item.name })) ?? [];

                if (this.options) {
                    // Mutate the array in-place so the component property updates by reference
                    this.options.length = 0;
                    this.options.push(...items);
                } else {
                    this.options = items;
                }

                return this.options;
            })
            .catch((error: any) => {
                console.error(`Error fetching lookup data of ${this.fieldName}:`, error);
                throw error;
            });
    }
    //---------------------------------------------------------  

    setData(id?: string, optionName?: string) {

        var optionId = id ? +id : 0;
        optionName = optionName ?? '';
        var items = [{ id: optionId, name: optionName }];
        if (this.options) {
            // Mutate the array in-place so the component property updates by reference
            this.options.length = 0;
            this.options.push(...items);
        } else {
            this.options = items;
        }
    }
    //---------------------------------------------------------  
}
