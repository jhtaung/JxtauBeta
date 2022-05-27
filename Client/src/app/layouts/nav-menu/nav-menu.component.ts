import { Component, OnInit } from '@angular/core';

interface IMenu {
  text: string;
  icon: string;
  routerLink?: string;
  children?: IMenuItem[];
}

interface IMenuItem {
  text: string;
  icon: string;
  routerLink: string;
}

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css'],
})
export class NavMenuComponent {
  menuList: IMenu[] = [
    {
      text: 'Home',
      icon: 'home',
      routerLink: '/',
    },
    {
      text: 'Account',
      icon: 'account_circle',
      routerLink: '/account',
    },
    {
      text: 'Sandbox',
      icon: 'account_circle',
      routerLink: '/sandbox',
    },
    {
      text: 'BAC',
      icon: 'folder',
      children: [
        {
          text: 'Appeals',
          icon: 'list',
          routerLink: '/bac/appeals',
        },
      ],
    },
    {
      text: 'Eform',
      icon: 'folder',
      children: [
        {
          text: 'Users',
          icon: 'list',
          routerLink: '/eform/users',
        },
        {
          text: 'USPS Address',
          icon: 'pin_drop',
          routerLink: '/eform/usps/address',
        },
      ],
    },
  ];

  constructor() {}
}
