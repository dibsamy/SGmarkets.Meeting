import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { TopNavComponent }      from './topnav';
import { SidebarComponent }     from './sidebar';

const _COMPONENTS = [
  TopNavComponent,
  SidebarComponent
]

@NgModule({
  imports: [CommonModule,
    RouterModule,
    FormsModule,
    ReactiveFormsModule
  ],
  exports:[_COMPONENTS],
  declarations: [_COMPONENTS]
})

export class SharedModule {
}
