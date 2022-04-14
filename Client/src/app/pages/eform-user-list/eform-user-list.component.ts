import { Component, OnInit, QueryList, ViewChildren } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { EformUserDto } from 'src/app/models/eform-user-list';
import { EformService } from 'src/app/services/eform.service';

@Component({
  selector: 'app-eform-user-list',
  templateUrl: './eform-user-list.component.html',
  styleUrls: ['./eform-user-list.component.css']
})
export class EformUserListComponent implements OnInit {

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
    { key: 'disabled', name: 'Disabled', attr: 'string' }
  ];

  dataSources: { name: string, dataSource: MatTableDataSource<EformUserDto> } [] = [
    { name: 'Dev List', dataSource: new MatTableDataSource()},
    { name: 'User List', dataSource: new MatTableDataSource() }
  ];

  @ViewChildren(MatPaginator) paginator = new QueryList<MatPaginator>();
  @ViewChildren(MatSort) sort = new QueryList<MatSort>();

  constructor(private eformService: EformService) { }

  ngOnInit(): void {
    this.setTable();
    this.load();
  }

  setTable() {
    for (var col of this.columns) {
      this.displayedColumns.push(col.key);
    }

    for (var d of this.dataSources) {
      d.dataSource.sortingDataAccessor = (data: any, sortHeaderId: string): string => {
        if (typeof data[sortHeaderId] === 'string') {
          return data[sortHeaderId].toLocaleLowerCase();
        }
        return data[sortHeaderId];
      };
    }
  }

  ngAfterViewInit() {
    for (var i = 0; i < this.dataSources.length; i++) {
      this.dataSources[i].dataSource.sort = this.sort.toArray()[i];
      this.dataSources[i].dataSource.paginator = this.paginator.toArray()[i];
    }
  }

  load() {
    this.isLoading = true;
    this.eformService.getUserList().subscribe({
      next: response => {
        console.log('response', response);
        var data = response.data!;

        var devs: string[] = [
          'admin',
          'jhtaung',
          'Guest',
          'mmurshed',
          'mwhelpley',
          'tmazumder'
        ];

        var devUserList: EformUserDto[] = [];
        var userList: EformUserDto[] = [];

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
      },
      error: error => {
        console.log('error', error);
      },
      complete: () => {
        this.isLoading = false;
      },
    });
  }

}
