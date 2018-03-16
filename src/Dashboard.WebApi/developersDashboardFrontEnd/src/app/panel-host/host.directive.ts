import {Directive, ViewContainerRef} from '@angular/core';

@Directive({
    selector: '[host]'
})
export class HostDirective {
    constructor(public viewContainerRef : ViewContainerRef) {
        console.log("View container ref: " + viewContainerRef);
    }
}