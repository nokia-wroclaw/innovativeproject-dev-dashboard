export interface Pipeline {
    id : number;
    ref : string;
    status : string;
    commitTitle : string,
    commiterName : string;
    commiterEmail : string;

    stages : Stage[];
}

export interface Stage {
    stageName : string;
    stageStatus : string;
}

export class SupportedProviders{
    data : string[];
    constructor(data : string[]) {}
}

export class Project {
    
    id : number;
    projectTitle : string;
    apiHostUrl : string;
    apiProjectId : string;
    apiAuthenticationToken : string;
    dataProviderName: string;
    ciDataUpdateCronExpression: string;
    staticPipelines : Pipeline[];
    dynamicPipelines : Pipeline[];

  constructor(projectTitle: string, apiHostUrl: string, apiProjectId: string, apiAuthenticationToken: string, dataProviderName: string, ciDataUpdateCronExpression: string, pipelines : Pipeline[]) {}

  setCiDataUpdateCronExpression(intervalMinutes: number) {
    this.ciDataUpdateCronExpression = `*/${intervalMinutes} * * * *`;
  }
}
