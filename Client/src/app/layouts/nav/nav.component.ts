import { Component, OnInit } from '@angular/core';
import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { Observable } from 'rxjs';
import { map, shareReplay } from 'rxjs/operators';
import { AccountService } from 'src/app/services/account.service';
import { User } from 'src/app/models/user';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css'],
})
export class NavComponent implements OnInit {
  title: string = 'Jxtau Beta';

  isHandset$: Observable<boolean> = this.breakpointObserver
    .observe(Breakpoints.Handset)
    .pipe(
      map(result => result.matches),
      shareReplay()
    );

  isLoggedIn: boolean = false;
  user!: User;

  constructor(
    private breakpointObserver: BreakpointObserver,
    private accountService: AccountService
  ) {}

  ngOnInit(): void {
    this.load();
  }

  load() {
    var user = this.accountService.getCurrentUser();
    if (user) {
      var temp = JSON.parse(user);
      this.user = { name: temp.name };
      this.isLoggedIn = true;
    } else {
      this.isLoggedIn = false;
    }
  }

  login() {
    this.accountService.login().subscribe({
      next: () => {
        this.load();
        return true;
      },
      error: error => {
        console.log('error', error);
        return false;
      },
    });
  }
}
