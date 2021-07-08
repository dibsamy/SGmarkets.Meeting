import { Component, OnInit } from '@angular/core';
import { NgbDateStruct } from '@ng-bootstrap/ng-bootstrap';
import { ReservationModel } from '@proxy/Sgmarkets-meeting/models';
import { ReservationService, RoomService } from '@proxy/Sgmarkets-meeting/services';

import { Observable, of } from 'rxjs';


@Component({
    selector: 'reservation',
    templateUrl: './reservation.component.html',
    styles: [`
        .required > label:after {
            content:" *";
            color: red;
        }
    `]
})

export class ReservationComponent implements OnInit {


    now: Date = new Date()
    nbDay: NgbDateStruct = <NgbDateStruct>{
        year: this.now.getFullYear(),
        month: this.now.getMonth() + 1,
        day: this.now.getDate()
    };

    rooms$: Observable<string[]> = of([])
    reservations$: Observable<Array<ReservationModel>> = of([])
    beginSlots: Array<string> = []
    endSlots: Array<string> = []

    reservation: ReservationModel = <ReservationModel>{
        beginTime: "01:00",
        endTime: "02:00",
        day: this.convertToDate(),
        roomName: 'room0'
    }

    constructor(
        private reservationService: ReservationService,
        private roomService: RoomService) {
    }

    ngOnInit(): void {

        this.roomService.getApiRoomList().subscribe(rooms => {
            this.reservation.roomName = rooms[0]
            this.rooms$ = of(rooms)
        })

        this.loadFreeSLots()

        this.loadReservations()
    }

    delete(reservation: ReservationModel) {
        this.reservationService
            .postApiReservationDelete(reservation)
            .subscribe(result => {
                this.loadFreeSLots()
                this.loadReservations()
            })
    }

    create() {

        this.reservation.day = this.convertToDate()

        this.reservationService
            .postApiReservationCreate(this.reservation)
            .subscribe(result => {
                this.reservation.endTime = ""
                this.reservation.beginTime = ""
                this.reservation.organizer = ""

                this.loadFreeSLots()
                this.loadReservations()
            })
    }

    canCreate() {
        return !this.reservation.organizer
            || !this.reservation.day
            || !this.reservation.roomName
            || !this.reservation.beginTime
            || !this.reservation.endTime
    }

    private loadReservations() {
        this.reservationService.getApiReservationList(this.convertToDate())
            .subscribe(reservations => {
                this.reservations$ = of(reservations)
            })
    }

    private loadFreeSLots() {
        this.endSlots = []
        this.beginSlots = []
        this.reservationService.getApiReservationFreeSlots(this.convertToDate())
            .subscribe(slots => {
                slots.forEach((slot: any) => {
                    let hour = slot.start.hours < 10 ? "0" + slot.start.hours : slot.start.hours;
                    if (slot.start.hours != 0) this.beginSlots.push(hour + ":00")
                    if (slot.start.hours != 1) this.endSlots.push(hour + ":00")
                });
            })
    }

    private convertToDate() {
        let y = this.nbDay.year
        let m = this.nbDay.month
        let d = this.nbDay.day;
        return y + "-" + (m < 10 ? ('0' + m) : m) + '-' + (d < 10 ? ('0' + d) : d) + 'T00:00:00'
    }

}