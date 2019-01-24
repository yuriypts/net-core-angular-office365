import { Injectable} from '@angular/core';


@Injectable()
export class StorageService {
    constructor() {
    }
    public getItem(key: string) {
        const data = localStorage.getItem(key);
        if (data === '' || data == null) {
            return null;
        }
        return data;
    }
    public setItem(key: string, data: string) {
        localStorage.setItem( key, data);
    }
    public deleteItem(key: string) {
        localStorage.removeItem(key);
    }
    public clearStorage() {
        localStorage.clear();
    }
}
