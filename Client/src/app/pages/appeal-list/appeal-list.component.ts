import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AppealList } from 'src/app/models/appeal-list';
import { AppealParams } from 'src/app/models/appeal-params';
import { AppealService } from 'src/app/services/appeal.service';

@Component({
  selector: 'app-appeal-list',
  templateUrl: './appeal-list.component.html',
  styleUrls: ['./appeal-list.component.css']
})
export class AppealListComponent implements OnInit {
  isLoading = false;
  orderBy = "";
  search: string = "";
  totalRows = 0;
  pageSize = 10;
  currentPage = 0;
  pageSizeOptions: number[] = [5, 10, 25, 100];

  displayedColumns: string[] = [];
  columns = [
    { key: 'actions', name: 'Actions', attr: 'actions' },
    { key: 'id', name: 'ID', attr: 'string' },
    { key: 'rap', name: 'RAP', attr: 'string' },
    { key: 'dept', name: 'DEPT', attr: 'string' },
    { key: 'mpid', name: 'MPID', attr: 'string' },
    { key: 'firstName', name: 'First Name', attr: 'string' },
    { key: 'lastName', name: 'Last Name', attr: 'string' },
    { key: 'meeting', name: 'Meeting', attr: 'date-1' },
    { key: 'status', name: 'Status', attr: 'string' },
    { key: 'statusUpdateUser', name: 'Updated User', attr: 'string' },
    { key: 'statusUpdateDate', name: 'Updated Date', attr: 'date-2' },
    { key: 'receivedDate', name: 'Received Date', attr: 'date-3' },
    { key: 'notes', name: 'Notes', attr: 'string' },
  ];

  appealParams: AppealParams;
  dataSource: MatTableDataSource<AppealList> = new MatTableDataSource();

  @ViewChild(MatPaginator)
  paginator!: MatPaginator;

  @ViewChild(MatSort) 
  sort!: MatSort;

  constructor(private appealService: AppealService) {
    this.appealParams = this.appealService.getAppealParams();
  }

  ngOnInit(): void {
    this.setTable();
    this.load();
  }

  setTable() {
    for (var col of this.columns) {
      this.displayedColumns.push(col.key);
    }
  }

  load() {
    this.isLoading = true;
    this.appealParams.pageNumber = this.currentPage + 1;
    this.appealParams.pageSize = this.pageSize;
    this.appealParams.orderBy = this.orderBy;
    this.appealParams.search = this.search;

    console.log(this.appealParams);

    this.appealService.setAppealParams(this.appealParams);
    this.appealService.getAppealList(this.appealParams).subscribe({
      next: response => {
        console.log('response', response);
        this.dataSource.data = response.result;
        this.currentPage = response.page.currentPage - 1;
        this.totalRows = response.page.totalItems;
      },
      error: error => {
        console.log(error);
      },
      complete: () => {
        this.isLoading = false;
      }
    });
  }

  pageChanged(event: PageEvent) {
    this.currentPage = event.pageIndex;
    this.pageSize = event.pageSize;
    this.load();
  }

  doSort(event: any) {
    this.orderBy = event.active + "+" + event.direction;
    this.load();
  }

  doFilter() {
    this.currentPage = 0;
    this.load();
  }
}
