import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-sandbox',
  templateUrl: './sandbox.component.html',
  styleUrls: ['./sandbox.component.css'],
})
export class SandboxComponent implements OnInit {
  input: string = 'input: ';
  output: string = 'output: ';

  constructor() {}

  ngOnInit(): void {
    this.main();
  }

  main() {
    let secret = 'VALID';
    let inputs = ['PRIME', 'SIVIE'];

    let map: any = {};
    for (let i = 0; i < secret.length; i++) {
      let letter = secret[i];
      if (letter in map) {
        map[letter].push(i);
      } else {
        map[letter] = [i];
      }
    }

    let results: any = {};
    for (let i = 0; i < inputs.length; i++) {
      let input = inputs[i];
      results[input] = '';

      // assign colors
      for (let j = 0; j < input.length; j++) {
        let letter = input[j];
        let color = 'R';
        if (map[letter]) {
          color = 'Y';
          if (map[letter].includes(j)) {
            color = 'G';
          }
        }
        results[input] += color;
      }

      //
      if (i > 0) {
        let first = inputs[0];
        let check = false;
        for (let k = 0; k < input.length; k++) {
          if (first[k] === 'G' && input[k] === 'G') {
            check = true;
            break;
          }
        }
        console.log(check);
      }
    }

    console.log(results);

  }
}
