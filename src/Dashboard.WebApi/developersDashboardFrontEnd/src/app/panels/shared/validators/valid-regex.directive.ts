import { Directive } from "@angular/core";
import { NG_VALIDATORS, AbstractControl, Validator } from "@angular/forms";

@Directive({
  selector: '[appRegexValid]',
  providers: [
    {
      provide: NG_VALIDATORS,
      useExisting: RegexValidatorDirective,
      multi: true
    }
  ]
})
export class RegexValidatorDirective implements Validator {

  validate(control : AbstractControl) : {
    [key : string]: any
  }
  {
    var isRegexInvalid = false;
    try {
      new RegExp(control.value);
    } catch (e) {
      isRegexInvalid = true;
    }

    return isRegexInvalid
      ? {
        'error': {
          message: "Regex is invalid."
        }
      }
      : null;
  }
}