import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { MatSnackBar } from '@angular/material';

export enum NotificationType {
  Success, Failure
}

export class SnackBar {
  message : string;
  type : NotificationType;
  constructor(message : string,
              type : NotificationType
            ){}

}

@Injectable()
export class NotificationService {

  constructor() { }

  public subj_notification: Subject<SnackBar> = new Subject();
  public snack = new SnackBar('',undefined);

  addNotification(message: string, type : NotificationType){

    this.snack.message = message;
    this.snack.type = type;
    
    this.subj_notification.next(this.snack);
  }

}
