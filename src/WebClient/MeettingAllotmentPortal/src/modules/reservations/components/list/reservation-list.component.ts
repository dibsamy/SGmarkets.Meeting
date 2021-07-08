import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ReservationModel } from '@proxy/Sgmarkets-meeting/models';
import { Observable, of } from 'rxjs';


@Component({
    selector: 'reservation-list',
    templateUrl: './reservation-list.component.html'
})

export class ReservationListComponent {

    @Input() room: string = ""

    @Input() day: string = ""

    @Input() reservations: Array<ReservationModel> | null = []

    @Output() public onDelete: EventEmitter<ReservationModel> = new EventEmitter()

    constructor() {
    }


    deleteHandler(reservation: ReservationModel) {
        this.onDelete.emit(reservation)
    }

}