import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';

@Component({
  selector: 'app-eform-doc-list',
  templateUrl: './eform-doc-list.component.html',
  styleUrls: ['./eform-doc-list.component.css'],
})
export class EformDocListComponent implements OnInit {
  isLoading: boolean = false;
  search: string = '';

  displayedColumns: any[] = [];
  columns: any[] = [];
  pageSize: number = 10;
  pageSizeOptions: number[] = [10, 20, 50, 100];
  dataSource: MatTableDataSource<any> = new MatTableDataSource();

  constructor() {}

  ngOnInit(): void {}

  doFilter(event: any) {}
}
