import { CommonModule } from '@angular/common';
import { NgModule, Optional, SkipSelf } from '@angular/core';

import { environment } from '../environments/environment';
import { ApiModule as MeetingApiModule } from './Sgmarkets-meeting/api.module';
import { ApiConfigurationInterface } from './Sgmarkets-meeting/api-configuration';


export function getConfigApi() {
    return <ApiConfigurationInterface>{
        rootUrl: environment.apiUrl
    }
}

@NgModule({
    imports: [
        CommonModule,
        MeetingApiModule.forRoot(getConfigApi()),
    ]
})

export class ProxyModule {

    constructor(@Optional() @SkipSelf() parentModule: ProxyModule) {
        if (parentModule) {
            throw new Error(
                'ProxyModule is already loaded. Import it in the AppModule only');
        }
    }
}