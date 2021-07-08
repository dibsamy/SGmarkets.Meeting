import { Component, OnInit, ViewEncapsulation } from '@angular/core';


@Component({
  selector: 'side-bar',
  templateUrl: './sidebar.html',
  styles:[`
  
  #sidebarMenu {
    margin-top:80px;
  }
    #sidebarMenu.active .sidebar-wrapper {
      left: 0;
    } 

    .sidebar-wrapper {
      width: 300px;
      height: 100vh;
      position: fixed;
      top: 0;
      z-index: 10;
      overflow-y: auto;
      background-color: #fff;
      bottom: 0;
      transition: left .5s ease-out;
  }

    .ps {
      overflow: hidden !important;
      overflow-anchor: none;
      -ms-overflow-style: none;
      touch-action: auto;
      -ms-touch-action: auto;
  }

    .sidebar-wrapper .position-sticky {
        margin-top: 2rem;
        font-size:18px
    }
  `]
})

export class SidebarComponent{
  constructor() { }
}