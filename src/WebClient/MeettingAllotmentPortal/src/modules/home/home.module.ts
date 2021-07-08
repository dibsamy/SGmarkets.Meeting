import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';


import { SharedModule } from '@shared/shared.module'

import { HomeRoutingModule } from './home-routing.module'
import { HomeComponent } from './home.component'

const _COMPONENTS = [
    HomeComponent
];


@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        //ROUTING
        HomeRoutingModule,
        SharedModule
    ],

    declarations: [_COMPONENTS],
    entryComponents: [_COMPONENTS],
    exports: [_COMPONENTS]
})

export class HomeModule { }