import { Component, OnInit } from '@angular/core';
import { AppModule } from 'src/app/app.module';
import { EformService } from 'src/app/services/eform.service';

@Component({
  selector: 'app-sandbox',
  templateUrl: './sandbox.component.html',
  styleUrls: ['./sandbox.component.css'],
})
export class SandboxComponent implements OnInit {
  input: string = 'input: ';
  output: string = 'output: ';

  constructor(private eformService: EformService) {}

  ngOnInit(): void {
    this.main();
    // this.pWordle();
    // this.pContacts();
    this.pTree();
  }

  main() {
    this.eformService.getEforms().subscribe({
      next: eforms => {
        this.eformService.getEformsBot().subscribe({
          next: eformsBot => {
            this.compare(eforms, eformsBot);
          },
          error: error => {
            console.log(error);
          },
        });
      },
      error: error => {
        console.log(error);
      },
    });
  }

  compare(eforms: string, eformsBot: string) {
    let eformsArr = eforms.split('\r\n');
    let eformsBotArr = eformsBot.split('\r\n');

    // eforms
    let found = [];
    let missing = [];
    for (let i = 0; i < eformsArr.length; i++) {
      let eform = eformsArr[i];
      if (eform === '') continue;
      const regex = /M\d{8}/;
      let index = eform.search(regex);

      if (index === -1) {
        console.log('!regex: ' + eform);
        continue;
      }

      // reformat
      let eformStr = eform.slice(0, index + 10);
      let eformDateStr = eform.slice(index + 10, eform.length);
      let dateStr = eformDateStr.replace(/[\:/]/g, '');
      let dateArr = eformDateStr.split(':');
      eformDateStr = dateArr[0] + ' ' + dateArr[1] + ':' + dateArr[2];
      let date = new Date(eformDateStr);
      let temp = eformStr + dateStr + '.pdf';
      let tempTime = { date, name: eformDateStr + '\t\t' + temp };

      // compare w/ prev minute as eforms db rounds up
      let datePrev = new Date(date.valueOf() - 1 * 60000);
      let hh = datePrev.getHours();
      let mm = datePrev.getMinutes();
      let ampm = hh > 12 ? 'PM' : 'AM';
      hh = hh > 12 ? hh - 12 : hh;
      let datePrevStr =
        (datePrev.getMonth() + 1).toString() +
        datePrev.getDate().toString() +
        datePrev.getFullYear().toString() +
        hh +
        (mm < 10 ? '0' + mm : mm) +
        ' ' +
        ampm;
      let eformPrev = eformStr + datePrevStr + '.pdf';

      // bind
      eformsBotArr.includes(temp) || eformsBotArr.includes(eformPrev)
        ? found.push(tempTime)
        : missing.push(tempTime);
    }

    // eforms bot
    let eBotArr = [];
    for (let i = 0; i < eformsBotArr.length; i++) {
      let eform = eformsBotArr[i];
      if (eform === '') continue;
      const regex = /M\d{8}/;
      let mpidIndex = eform.search(regex);

      if (mpidIndex === -1) {
        console.log('!regex: ' + eform);
        continue;
      }

      // MMDDYYYYHHMM AM/PM
      let eformTime = eform.slice(mpidIndex + 10);

      // MMDDYYYY or MDDYYYY or MDYYYY
      // 11 = jan-1 or nov-DD, 111 = jan-11 or nov-1, 121 = jan-21 or dec-1
      let yyyy = '2022';
      let yearIndex = eformTime.indexOf(yyyy);
      let mmdd = eformTime.slice(0, yearIndex);
      mmdd = mmdd.slice(0, 1) + '/' + mmdd.slice(1);
      let mmddyyyy = mmdd + '/' + yyyy;

      // HHMM or HMM
      // 111 = 0101 or 1101, 121 = 0121, 1201 = 1201
      let hhmm = '';
      let hhmmArr = eformTime.slice(yearIndex + 4).split(' ');
      if (hhmmArr[0].length == 3) {
        hhmm =
          hhmmArr[0].slice(0, 1) +
          ':' +
          hhmmArr[0].slice(1, 3) +
          ' ' +
          hhmmArr[1].split('.')[0];
      } else if (hhmmArr[0].length == 4) {
        hhmm =
          hhmmArr[0].slice(0, 2) +
          ':' +
          hhmmArr[0].slice(2, 4) +
          ' ' +
          hhmmArr[1].split('.')[0];
      }
      let dateStr = mmddyyyy + ' ' + hhmm;
      let date = new Date(dateStr);
      eBotArr.push({ date, name: mmddyyyy + ' ' + hhmm + '\t\t' + eform });
    }

    // sort
    found.sort((a, b) => b.date.valueOf() - a.date.valueOf());
    missing.sort((a, b) => b.date.valueOf() - a.date.valueOf());
    eBotArr.sort((a, b) => b.date.valueOf() - a.date.valueOf());

    // bind input
    this.input = 'missing - ' + missing.length + '\n';
    for (let i = 0; i < missing.length; i++) {
      this.input += missing[i].name + '\n';
    }
    this.input += '\nfound - ' + found.length + '\n';
    for (let i = 0; i < found.length; i++) {
      this.input += found[i].name + '\n';
    }

    // bind output
    this.output = '';
    this.output += 'bot - ' + eBotArr.length + '\n';
    for (let i = 0; i < eBotArr.length; i++) {
      this.output += eBotArr[i].name + '\n';
    }
  }

  pWordle() {
    let secret = 'VALID';
    let inputs = ['PRIME', 'SDVME'];

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
    let firstMap: any = {};
    for (let i = 0; i < inputs.length; i++) {
      let input = inputs[i];
      results[input] = '';

      // assign colors
      for (let j = 0; j < input.length; j++) {
        let letter = input[j];
        let color = 'R';
        // check letter exists
        if (map[letter]) {
          color = 'Y';
          let mapSet = new Set(map[letter]);
          // check letter is in same place
          if (mapSet.has(j)) {
            color = 'G';
          }
        }
        if (i === 0) {
          firstMap[letter] = color;
        }
        results[input] += color;
      }

      // compare first w/ subsequent inputs
      if (i > 0) {
        let second = results[inputs[i]];
        let check = false;
        for (let k = 0; k < input.length; k++) {
          if (second[k] !== 'R' && firstMap[input[k]] === second[k]) {
            check = true;
            break;
          }
        }
        console.log(check);
      }
    }
    console.log(secret);
    console.log(inputs);
    console.log(results);
  }

  pContacts() {
    let queries = ['add hack', 'add hackerrank', 'find hac', 'find hak'];

    let root = this.pContactsNode();
    let pn: any;
    let r = [];
    for (let i = 0; i < queries.length; i++) {
      let q = queries[i].split(' ');
      switch (q[0]) {
        case 'add':
          pn = this.pContactsAdd(root, pn, q[1]);
          break;
        case 'find':
          r.push(this.pContactsFind(root, pn, q[1]));
          break;
      }
    }
    console.log(r);
  }

  pContactsNode() {
    return [{}, 0, 0];
  }

  pContactsAdd(root: any, pn: any, name: string) {
    pn = root;
    for (let i = 0; i < name.length; i++) {
      let c = name[i];
      if (!pn[0][c]) pn[0][c] = this.pContactsNode();
      pn[0][c][1]++;
      pn = pn[0][c];

      /*
      console.log(c);
      console.log(pn);
      console.log('--');
      */
    }
    pn[2] = 1;
    return pn;
  }

  pContactsFind(root: any, pn: any, name: string) {
    pn = root;
    for (let i = 0; i < name.length; i++) {
      let c = name[i];
      if (!pn[0][c]) return 0;
      pn = pn[0][c];

      /*
      console.log(c);
      console.log(pn);
      console.log('--');
      */
    }
    return pn[1];
  }

  pTree() {
    let tree = new BinaryTree<number>();
    tree.addNode(7);
    tree.addNode(8);
    tree.addNode(4);
    tree.addNode(3);
    tree.addNode(5);
    tree.addNode(2);
    tree.addNode(9);
    tree.addNode(11);
    console.log(tree);
    console.log('bfs');
    tree.bfsTraversal();
    console.log('dfs');
    tree.dfsTraversal();
  }
}

// A queue that can be of type T
// Generics are great in any language
class Queue<T> {
  private items: T[] = [];
  // Add and pop do the same thing
  // One has the fat arrow syntax
  public add = (item: T) => this.items.push(item);

  public pop(): T {
    return this.items.shift()!;
  }

  isEmpty(): boolean {
    return this.items.length == 0;
  }
}

class TreeNode<T> {
  private key: T;
  private leftChild?: TreeNode<T>;
  private rightChild?: TreeNode<T>;

  constructor(key: T) {
    this.key = key;
  }

  getKey = (): T => this.key;

  getLeftChild = (): TreeNode<T> => this.leftChild!;
  getRightChild = (): TreeNode<T> => this.rightChild!;

  setLeftChild = (leftNode: TreeNode<T>) => (this.leftChild = leftNode);
  setRightChild = (rightChild: TreeNode<T>) => (this.rightChild = rightChild);
}

// let's just traverse everything from
// the root node
class BinaryTree<H> {
  private rootNode: TreeNode<H> | undefined;

  setRoot = (node: TreeNode<H>) => (this.rootNode = node);

  public addNode(key: H): void {
    this.rootNode = this.addNodeByRecursion(this.rootNode!, key);
  }

  public addNodeByRecursion(currentNode: TreeNode<H>, key: H): TreeNode<H> {
    if (currentNode == null) {
      return new TreeNode<H>(key);
    }
    if (key < currentNode.getKey()) {
      currentNode.setLeftChild(
        this.addNodeByRecursion(currentNode.getLeftChild(), key)
      );
    } else if (key > currentNode.getKey()) {
      currentNode.setRightChild(
        this.addNodeByRecursion(currentNode.getRightChild(), key)
      );
    }
    return currentNode;
  }

  public bfsTraversal() {
    if (this.rootNode == null) {
      return;
    }

    let nodes: Queue<TreeNode<H>> = new Queue<TreeNode<H>>();
    nodes.add(this.rootNode);

    while (!nodes.isEmpty()) {
      let currentNode: TreeNode<H> = nodes.pop();

      console.log('key is: ' + currentNode.getKey());

      if (currentNode.getLeftChild() != null) {
        nodes.add(currentNode.getLeftChild());
      }

      if (currentNode.getRightChild() != null) {
        nodes.add(currentNode.getRightChild());
      }
    }
  }

  public dfsTraversal() {
    this.dfsTraversalRecursion(this.rootNode!);
  }

  public dfsTraversalRecursion(node: TreeNode<H>) {
    if (node === undefined) return;
    // console.log('key is : ' + node.getKey()); // pre order
    this.dfsTraversalRecursion(node.getLeftChild());
    console.log('key is : ' + node.getKey()); // in order
    this.dfsTraversalRecursion(node.getRightChild());
  }
}
