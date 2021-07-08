import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';


//Patient Routing
import { RoomRoutingModule } from './room-routing.module';

import { RoomListComponent } from './list/room-list.component'

const _COMPONENTS = [
    RoomListComponent
];


@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        //ROUTING
        RoomRoutingModule
    ],

    declarations: [_COMPONENTS],
    entryComponents: [_COMPONENTS],
    exports: [_COMPONENTS]
})

export class RoomModule { }