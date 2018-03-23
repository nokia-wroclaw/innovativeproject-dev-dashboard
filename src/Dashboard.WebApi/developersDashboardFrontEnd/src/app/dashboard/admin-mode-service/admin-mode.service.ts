import {Injectable} from '@angular/core';
import {BehaviorSubject} from 'rxjs/BehaviorSubject';

@Injectable()
export class AdminModeService {

    private adminModeSource = new BehaviorSubject < boolean > (false);
    adminMode = this
        .adminModeSource
        .asObservable();

    constructor() {}

    setAdminMode(adminMode : boolean) {
        this
            .adminModeSource
            .next(adminMode);
    }

}