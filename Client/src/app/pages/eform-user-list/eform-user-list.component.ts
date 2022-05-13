import {
  Component,
  OnInit,
  AfterViewInit,
  QueryList,
  ViewChildren,
} from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { lastValueFrom } from 'rxjs';
import { EformUserDto } from 'src/app/models/eform-user-list';
import { EformService } from 'src/app/services/eform.service';

@Component({
  selector: 'app-eform-user-list',
  templateUrl: './eform-user-list.component.html',
  styleUrls: ['./eform-user-list.component.css'],
})
export class EformUserListComponent implements OnInit, AfterViewInit {
  log: string = '';
  private cancel: boolean = false;
  private refresh: boolean = false;

  timeDisplay: string = '0 s';
  private time: number = 0;
  private interval: any;

  isLoading = false;
  pageSize = 10;
  pageSizeOptions: number[] = [10, 25, 50, 100, 200];

  displayedColumns: string[] = [];
  columns = [
    { key: 'actions', name: 'Actions', attr: 'actions' },
    { key: 'id', name: 'ID', attr: 'string' },
    { key: 'username', name: 'User Name', attr: 'string' },
    { key: 'firstName', name: 'First Name', attr: 'string' },
    { key: 'lastName', name: 'Last Name', attr: 'string' },
    { key: 'disabled', name: 'Disabled', attr: 'string' },
    { key: 'log', name: 'Log', attr: 'string' },
  ];

  dataSources: {
    name: string;
    dataSource: MatTableDataSource<EformUserDto>;
  }[] = [
    { name: 'Dev List', dataSource: new MatTableDataSource() },
    { name: 'User List', dataSource: new MatTableDataSource() },
  ];

  @ViewChildren(MatPaginator) paginator = new QueryList<MatPaginator>();
  @ViewChildren(MatSort) sort = new QueryList<MatSort>();

  private devs: string[] = [
    'admin',
    'jhtaung',
    'Guest',
    'mmurshed',
    'mwhelpley',
    'tmazumder',
  ];

  constructor(private eformService: EformService) {}

  ngOnInit() {
    this.setTable();
    this.load();
  }

  async ngAfterViewInit() {
    for (var i = 0; i < this.dataSources.length; i++) {
      this.dataSources[i].dataSource.sort = this.sort.toArray()[i];
      this.dataSources[i].dataSource.paginator = this.paginator.toArray()[i];
    }
  }

  setTable() {
    for (var col of this.columns) {
      this.displayedColumns.push(col.key);
    }

    for (var d of this.dataSources) {
      d.dataSource.sortingDataAccessor = (
        data: any,
        sortHeaderId: string
      ): string => {
        if (typeof data[sortHeaderId] === 'string') {
          return data[sortHeaderId].toLocaleLowerCase();
        }
        return data[sortHeaderId];
      };
    }
  }

  load() {
    this.isLoading = true;
    this.eformService.getUserList().subscribe({
      next: response => {
        var devs = this.devs;
        var devUserList: EformUserDto[] = [];
        var userList: EformUserDto[] = [];

        this.log = response.nextLink ?? '';
        var data = response.data!;
        for (var i = 0; i < data.length; i++) {
          var user = data[i];
          if (devs.includes(user.username)) {
            devUserList = devUserList.concat(user);
          } else {
            userList = userList.concat(user);
          }
        }

        this.dataSources[0].dataSource.data = devUserList;
        this.dataSources[1].dataSource.data = userList;

        // this.deleteUsers();
      },
      error: error => {
        console.log('error', error);
      },
      complete: () => {
        this.isLoading = false;
      },
    });
  }

  startTimer() {
    console.log('=====>');
    this.interval = setInterval(() => {
      if (this.time === 0) {
        this.time++;
      } else {
        this.time++;
      }
      this.timeDisplay = this.transformTimer(this.time);
    }, 1000);
  }

  transformTimer(value: number): string {
    const minutes = Math.floor(value / 60);
    const seconds = value - minutes * 60 + ' s';
    return minutes > 1 ? minutes + ' m ' + seconds : seconds;
  }

  pauseTimer() {
    clearInterval(this.interval);
  }

  async deleteUsers() {
    // start countdown
    this.startTimer();
    this.cancel = false;
    this.log += 'config: refresh = ' + this.refresh + '\nstart\n';
    this.log += 'start\ncountdown\n';
    var countdown = 5;
    for (var i = 0; i < countdown; i++) {
      this.log += countdown - i + '\n';
      await new Promise(resolve => setTimeout(resolve, 1000));
      if (this.cancel) {
        this.pauseTimer();
        return;
      }
    }

    // start delete
    this.cancel = false;
    this.log += '\nmass delete users - start \n';

    // get user list
    var userList = this.dataSources[1].dataSource.data;
    var length = userList.length;
    length = 2;
    this.log += 'user count: ' + length + '\n';
    // console.log(userList);

    // iterate user list
    for (var i = 0; i < length; i++) {
      if (this.cancel) {
        break;
      }
      var user = userList[i];

      // log
      this.dataSources[1].dataSource.data[i].log = 'deleting user...';
      this.log += 'deleting user: ' + user.username + '...';

      // delete user
      await new Promise(resolve => setTimeout(resolve, 1000));
      const response$ = this.eformService.getUser(user.id); // deleteUser(user.id);
      const response = await lastValueFrom(response$);

      // error
      if (response === null) {
        const error = 'error: invalid user id.';
        console.log(error + ' ' + user.id);
        this.dataSources[1].dataSource.data[i].log = error;
        this.log += error + '\n';
        continue;
      }

      // update log
      console.log(response);
      this.dataSources[1].dataSource.data[i].log = 'deleted user';
      this.log += 'done\n';
    }
    this.log += 'mass delete users - finish\n\n';

    // refresh countdown
    this.log += 'refresh = ' + this.refresh + '\nstop\ncountdown\n';
    for (var i = 0; i < countdown; i++) {
      this.log += countdown - i + '\n';
      await new Promise(resolve => setTimeout(resolve, 1000));
    }

    if (this.cancel || !this.refresh) {
      this.pauseTimer();
      return;
    }

    // reload
    window.location.reload();
  }

  cancelProcess() {
    this.log += '\nfinishing last process...\n';
    this.log += 'cancelling...\n';
    this.cancel = true;
  }
}
