
export enum SuccessMessage {
    PANEL_SAVED,
    PROJECT_SAVED
    
}

export enum FailureMessage {
    FORM_SAVE_FAILED,
    INVALID_SPECIFIC_CONFIGURATION,
    FETCH_PANELS_FAILED,
    PROJECT_SAVED_FAILED
    
}

export const successMessages : Map<SuccessMessage, string> = new Map(
    [
        [SuccessMessage.PANEL_SAVED, "Panel saved"],
        [SuccessMessage.PROJECT_SAVED, "Project saved"],

    ]
)

export const failureMessages : Map<FailureMessage, string> = new Map(
    [
        [FailureMessage.FORM_SAVE_FAILED, "Couldn't save the form"],
        [FailureMessage.INVALID_SPECIFIC_CONFIGURATION, "Panel specific configuration is invalid"],
        [FailureMessage.FETCH_PANELS_FAILED, "Couldn't fetch panels from the server"],
        [FailureMessage.PROJECT_SAVED_FAILED, "Couldn't save the project"],
    ]
)