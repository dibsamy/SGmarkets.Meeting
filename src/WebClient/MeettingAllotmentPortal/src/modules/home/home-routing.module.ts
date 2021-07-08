import { NgModule } from '@angular/core';
import { Route, RouterModule } from '@angular/router';
import { HomeComponent } from './home.component';



export const routes: Route[] = [
    {
        path: 'meeting', component: HomeComponent,
        children: [
            { path: 'reservations', loadChildren: () => import('../reservations/reservation.module').then(m => m.ReservationModule), data: { title: 'Meeting - Reservations', metaDescription: 'Meeting - Reservations' } },
            { path: 'rooms', loadChildren: () => import('../rooms/rooms.module').then(m => m.RoomModule), data: { title: 'Meeting - Rooms', metaDescription: 'Meeting -  Rooms' } },
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})

export class HomeRoutingModule {

}
