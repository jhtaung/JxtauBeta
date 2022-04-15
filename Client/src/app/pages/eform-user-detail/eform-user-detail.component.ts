import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';

import { EformUserDetailDto } from 'src/app/models/eform-user-detail';
import { EformService } from 'src/app/services/eform.service';

interface JxRowDto {
  key: string;
  value: string;
}

@Component({
  selector: 'app-eform-user-detail',
  templateUrl: './eform-user-detail.component.html',
  styleUrls: ['./eform-user-detail.component.css'],
})
export class EformUserDetailComponent implements OnInit {
  id: string = '';
  error: string = '';
  isLoading = false;

  pageSize = 50;
  pageSizeOptions: number[] = [10, 25, 50, 100];
  displayedColumns: string[] = ['key', 'value'];
  dataSource: MatTableDataSource<JxRowDto> = new MatTableDataSource();
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(
    private route: ActivatedRoute,
    private eformService: EformService
  ) {}

  ngOnInit(): void {
    this.load();
  }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  load() {
    this.route.params.subscribe((params) => {
      this.id = params['id'];
      this.eformService.getUser(this.id).subscribe({
        next: (response) => {
          this.isLoading = true;
          this.dataSource.data = this.parse(response);
        },
        error: (error) => {
          console.log('error', error);
          this.error = 'error loading page. view console.';
        },
        complete: () => {
          this.isLoading = false;
        },
      });
    });
  }

  parse(data: EformUserDetailDto): JxRowDto[] {
    var result = [];
    const entiries = Object.entries(data);
    for (const [k, v] of entiries) {
      const str = k.toString();
      const res = str.replace(/([A-Z])/g, ' $1');
      const fin = res.charAt(0).toUpperCase() + res.slice(1);
      var key = fin;
      var value = v;
      switch (typeof v) {
        case 'string':
          break;
        case 'boolean':
          value = v ? 'true' : 'false';
          break;
        case 'object':
          value = JSON.stringify(v);
          break;
        default:
          break;
      }
      result.push({ key: key, value: value });
    }
    return result;
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }
}
