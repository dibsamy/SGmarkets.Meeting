import { Component, OnInit } from '@angular/core';
import { RoomService } from '@proxy/Sgmarkets-meeting/services';
import { Observable, of } from 'rxjs';


@Component({
    selector: 'room-list',
    templateUrl: './room-list.component.html'
})

export class RoomListComponent implements OnInit {

    rooms$: Observable<string[]> = of([])

    constructor(private roomService: RoomService) { }

    ngOnInit(): void {
        this.roomService
            .getApiRoomList()
            .subscribe(rooms => {
                this.rooms$ = of(rooms)
            })
    }
}