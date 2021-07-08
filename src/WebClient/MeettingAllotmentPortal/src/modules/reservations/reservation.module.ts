import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

//Patient Routing
import { ReservationRoutingModule } from './reservation-routing.module';


import { ReservationComponent } from './reservation.component';
import { ReservationListComponent } from './components/list/reservation-list.component'


const _COMPONENTS = [
    ReservationComponent,
    ReservationListComponent
];


@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        NgbModule,
        //ROUTING
        ReservationRoutingModule
    ],

    declarations: [_COMPONENTS],
    entryComponents: [_COMPONENTS],
    exports: [_COMPONENTS]
})

export class ReservationModule { }