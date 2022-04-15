import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AccountService } from 'src/app/services/account.service';

interface JxRowDto {
  name: string;
}

@Component({
  selector: 'app-account',
  templateUrl: './account.component.html',
  styleUrls: ['./account.component.css'],
})
export class AccountComponent implements OnInit {
  error: string = '';
  isLoading = false;
  pageSize = 50;
  pageSizeOptions: number[] = [10, 25, 50, 100];

  displayedColumns: string[] = ['name'];
  dataSource: MatTableDataSource<JxRowDto> = new MatTableDataSource();
  dataSource2: MatTableDataSource<JxRowDto> = new MatTableDataSource();
  @ViewChild('paginatorTop') topPaginator!: MatPaginator;
  @ViewChild('paginatorBottom') bottomPaginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(private accountService: AccountService) {}

  ngOnInit(): void {
    this.load();
  }

  ngAfterViewInit() {
    this.dataSource.paginator = this.topPaginator;
    this.dataSource2.paginator = this.bottomPaginator;

    this.topPaginator.length = this.dataSource.data.length;
    this.bottomPaginator.length = this.dataSource2.data.length;

    this.dataSource.sort = this.sort;
  }

  load() {
    this.accountService.getGroupList().subscribe({
      next: (response) => {
        // console.log(response);
        var result: JxRowDto[] = [];
        for (const i in response) {
          result.push({ name: response[i] });
        }
        this.dataSource.data = result;
        this.dataSource2.data = result;
      },
      error: (error) => {
        console.log('error', error);
      },
      complete: () => {},
    });
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
    this.dataSource2.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }

    if (this.dataSource2.paginator) {
      this.dataSource2.paginator.firstPage();
    }
  }

  public handlePageTop(e: any) {
    let { pageSize } = e;
    this.bottomPaginator.pageSize = pageSize;
    if (!this.topPaginator.hasNextPage()) {
      this.bottomPaginator.lastPage();
    } else if (!this.topPaginator.hasPreviousPage()) {
      this.bottomPaginator.firstPage();
    } else {
      if (this.topPaginator.pageIndex < this.bottomPaginator.pageIndex) {
        this.bottomPaginator.previousPage();
      } else if (this.topPaginator.pageIndex > this.bottomPaginator.pageIndex) {
        this.bottomPaginator.nextPage();
      }
    }
  }

  public handlePageBottom(e: any) {
    if (!this.bottomPaginator.hasNextPage()) {
      this.topPaginator.lastPage();
    } else if (!this.bottomPaginator.hasPreviousPage()) {
      this.topPaginator.firstPage();
    } else {
      if (this.bottomPaginator.pageIndex < this.topPaginator.pageIndex) {
        this.topPaginator.previousPage();
      } else if (this.bottomPaginator.pageIndex > this.topPaginator.pageIndex) {
        this.topPaginator.nextPage();
      }
    }
  }
}
