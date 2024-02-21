# JavaScript Style Guide() {

## References

- Use `const` for all of your references; avoid using `var`. 

  > Why? This ensures that you can’t reassign your references, which can lead to bugs and difficult to comprehend code.

  ```javascript
  // bad
  var a = 1;
  var b = 2;

  // good
  const a = 1;
  const b = 2;
  ```

- If you must reassign references, use `let` instead of `var`. 
  > Why? `let` is block-scoped rather than function-scoped like `var`.

  ```javascript
  // bad
  var count = 1;
  if (true) {
    count += 1;
  }

  // good, use the let.
  let count = 1;
  if (true) {
    count += 1;
  }
  ```

## Objects

- Use the literal syntax for object creation.

  ```javascript
  // bad
  const item = new Object();

  // good
  const item = {};
  ```

- Use property value shorthand.
- 
  > Why? It is shorter and descriptive.

  ```javascript
  const lukeSkywalker = 'Luke Skywalker';

  // bad
  const obj = {
    lukeSkywalker: lukeSkywalker,
  };

  // good
  const obj = {
    lukeSkywalker,
  };
  ```


## Arrays

- Use the literal syntax for array creation.

  ```javascript
  // bad
  const items = new Array();

  // good
  const items = [];
  ```

- Use [Array#push](https://developer.mozilla.org/en/docs/Web/JavaScript/Reference/Global_Objects/Array/push) instead of direct assignment to add items to an array.

  ```javascript
  const someStack = [];

  // bad
  someStack[someStack.length] = 'abracadabra';

  // good
  someStack.push('abracadabra');
  ```


- Use array spreads `...` to copy arrays.

  ```javascript
  // bad
  const len = items.length;
  const itemsCopy = [];
  let i;

  for (i = 0; i < len; i += 1) {
    itemsCopy[i] = items[i];
  }

  // good
  const itemsCopy = [...items];
  ```

- To convert an iterable object to an array, use spreads `...` instead of [`Array.from`](https://developer.mozilla.org/en/docs/Web/JavaScript/Reference/Global_Objects/Array/from)

  ```javascript
  const foo = document.querySelectorAll('.foo');

  // good
  const nodes = Array.from(foo);

  // best
  const nodes = [...foo];
  ```

- Use line breaks after opening array brackets and before closing array brackets, if an array has multiple lines

  ```javascript
  // bad
  const arr = [
    [0, 1], [2, 3], [4, 5],
  ];

  const objectInArray = [{
    id: 1,
  }, {
    id: 2,
  }];

  const numberInArray = [
    1, 2,
  ];

  // good
  const arr = [[0, 1], [2, 3], [4, 5]];

  const objectInArray = [
    {
      id: 1,
    },
    {
      id: 2,
    },
  ];

  const numberInArray = [
    1,
    2,
  ];
  ```

- Use array destructuring.

  ```javascript
  const arr = [1, 2, 3, 4];

  // bad
  const first = arr[0];
  const second = arr[1];

  // good
  const [first, second] = arr;
  ```

## Strings

- Use single quotes `''` for strings.
  ```javascript
  // bad
  const name = "Capt. Janeway";

  // bad - template literals should contain interpolation or newlines
  const name = `Capt. Janeway`;

  // good
  const name = 'Capt. Janeway';
  ```

- Strings that cause the line to go over 100 characters should not be written across multiple lines using string concatenation.

  > Why? Broken strings are painful to work with and make code less searchable.

  ```javascript
  // bad
  const errorMessage = 'This is a super long error that was thrown because \
  of Batman. When you stop to think about how Batman had anything to do \
  with this, you would get nowhere \
  fast.';

  // bad
  const errorMessage = 'This is a super long error that was thrown because ' +
    'of Batman. When you stop to think about how Batman had anything to do ' +
    'with this, you would get nowhere fast.';

  // good
  const errorMessage = 'This is a super long error that was thrown because of Batman. When you stop to think about how Batman had anything to do with this, you would get nowhere fast.';
  ```

- When programmatically building up strings, use template strings instead of concatenation. eslint: [`prefer-template`](https://eslint.org/docs/rules/prefer-template) [`template-curly-spacing`](https://eslint.org/docs/rules/template-curly-spacing)

  > Why? Template strings give you a readable, concise syntax with proper newlines and string interpolation features.

  ```javascript
  // bad
  function sayHi(name) {
    return 'How are you, ' + name + '?';
  }

  // bad
  function sayHi(name) {
    return ['How are you, ', name, '?'].join();
  }

  // bad
  function sayHi(name) {
    return `How are you, ${ name }?`;
  }

  // good
  function sayHi(name) {
    return `How are you, ${name}?`;
  }
  ```

- Never use `eval()` on a string, it opens too many vulnerabilities. eslint: [`no-eval`](https://eslint.org/docs/rules/no-eval)

- Do not unnecessarily escape characters in strings.

  > Why? Backslashes harm readability, thus they should only be present when necessary.

  ```javascript
  // bad
  const foo = '\'this\' \i\s \"quoted\"';

  // good
  const foo = '\'this\' is "quoted"';
  const foo = `my name is '${name}'`;
  ```

## Functions

- Use named function expressions instead of function declarations.

  > Why? Function declarations are hoisted, which means that it’s easy - too easy - to reference the function before it is defined in the file. This harms readability and maintainability. If you find that a function’s definition is large or complex enough that it is interfering with understanding the rest of the file, then perhaps it’s time to extract it to its own module! Don’t forget to explicitly name the expression, regardless of whether or not the name is inferred from the containing variable (which is often the case in modern browsers or when using compilers such as Babel). This eliminates any assumptions made about the Error’s call stack. ([Discussion](https://github.com/airbnb/javascript/issues/794))

  ```javascript
  // bad
  function foo() {
    // ...
  }

  // bad
  const foo = function () {
    // ...
  };

  // good
  // lexical name distinguished from the variable-referenced invocation(s)
  const short = function longUniqueMoreDescriptiveLexicalFoo() {
    // ...
  };
  ```

- [7.4](#functions--note-on-blocks) **Note:** ECMA-262 defines a `block` as a list of statements. A function declaration is not a statement.

  ```javascript
  // bad
  if (currentUser) {
    function test() {
      console.log('Nope.');
    }
  }

  // good
  let test;
  if (currentUser) {
    test = () => {
      console.log('Yup.');
    };
  }
  ```

- Never name a parameter `arguments`. This will take precedence over the `arguments` object that is given to every function scope.

  ```javascript
  // bad
  function foo(name, options, arguments) {
    // ...
  }

  // good
  function foo(name, options, args) {
    // ...
  }
  ```

- Use default parameter syntax rather than mutating function arguments.

  ```javascript
  // really bad
  function handleThings(opts) {
    // No! We shouldn’t mutate function arguments.
    // Double bad: if opts is falsy it'll be set to an object which may
    // be what you want but it can introduce subtle bugs.
    opts = opts || {};
    // ...
  }

  // still bad
  function handleThings(opts) {
    if (opts === void 0) {
      opts = {};
    }
    // ...
  }

  // good
  function handleThings(opts = {}) {
    // ...
  }
  ```

- Always put default parameters last.

  ```javascript
  // bad
  function handleThings(opts = {}, name) {
    // ...
  }

  // good
  function handleThings(name, opts = {}) {
    // ...
  }
  ```

- Spacing in a function signature.
- 
  > Why? Consistency is good, and you shouldn’t have to add or remove a space when adding or removing a name.

  ```javascript
  // bad
  const f = function(){};
  const g = function (){};
  const h = function() {};

  // good
  const x = function () {};
  const y = function a() {};
  ```

- Never reassign parameters.

  > Why? Reassigning parameters can lead to unexpected behavior, especially when accessing the `arguments` object. It can also cause optimization issues, especially in V8.

  ```javascript
  // bad
  function f1(a) {
    a = 1;
    // ...
  }

  function f2(a) {
    if (!a) { a = 1; }
    // ...
  }

  // good
  function f3(a) {
    const b = a || 1;
    // ...
  }

  function f4(a = 1) {
    // ...
  }
  ```

- Functions with multiline signatures, or invocations, should be indented just like every other multiline list in this guide: with each item on a line by itself, with a trailing comma on the last item.

  ```javascript
  // bad
  function foo(bar,
               baz,
               quux) {
    // ...
  }

  // good
  function foo(
    bar,
    baz,
    quux
  ) {
    // ...
  }

  // bad
  console.log(foo,
    bar,
    baz);

  // good
  console.log(
    foo,
    bar,
    baz,
  );
  ```

## Arrow Functions

- When you must use an anonymous function (as when passing an inline callback), use arrow function notation.

  > Why? It creates a version of the function that executes in the context of `this`, which is usually what you want, and is a more concise syntax.

  > Why not? If you have a fairly complicated function, you might move that logic out into its own named function expression.

  ```javascript
  // bad
  [1, 2, 3].map(function (x) {
    const y = x + 1;
    return x * y;
  });

  // good
  [1, 2, 3].map((x) => {
    const y = x + 1;
    return x * y;
  });
  ```

## Classes & Constructors

- Always use `class`. Avoid manipulating `prototype` directly.

  > Why? `class` syntax is more concise and easier to reason about.

  ```javascript
  // bad
  function Queue(contents = []) {
    this.queue = [...contents];
  }
  Queue.prototype.pop = function () {
    const value = this.queue[0];
    this.queue.splice(0, 1);
    return value;
  };

  // good
  class Queue {
    constructor(contents = []) {
      this.queue = [...contents];
    }
    pop() {
      const value = this.queue[0];
      this.queue.splice(0, 1);
      return value;
    }
  }
  ```

- Use `extends` for inheritance.

  > Why? It is a built-in way to inherit prototype functionality without breaking `instanceof`.

  ```javascript
  // bad
  const inherits = require('inherits');
  function PeekableQueue(contents) {
    Queue.apply(this, contents);
  }
  inherits(PeekableQueue, Queue);
  PeekableQueue.prototype.peek = function () {
    return this.queue[0];
  };

  // good
  class PeekableQueue extends Queue {
    peek() {
      return this.queue[0];
    }
  }
  ```

- Methods can return `this` to help with method chaining.

  ```javascript
  // bad
  Jedi.prototype.jump = function () {
    this.jumping = true;
    return true;
  };

  Jedi.prototype.setHeight = function (height) {
    this.height = height;
  };

  const luke = new Jedi();
  luke.jump(); // => true
  luke.setHeight(20); // => undefined

  // good
  class Jedi {
    jump() {
      this.jumping = true;
      return this;
    }

    setHeight(height) {
      this.height = height;
      return this;
    }
  }

  const luke = new Jedi();

  luke.jump()
    .setHeight(20);
  ```

- Class methods should use `this`
  ```javascript
  // bad
  class Foo {
    bar() {
      console.log('bar');
    }
  }

  // good - this is used
  class Foo {
    bar() {
      console.log(this.bar);
    }
  }
  ```

## Modules

- Always use modules (`import`/`export`) over a non-standard module system. You can always transpile to your preferred module system.

  > Why? Modules are the future, let’s start using the future now.

  ```javascript
  // bad
  const AirbnbStyleGuide = require('./AirbnbStyleGuide');
  module.exports = AirbnbStyleGuide.es6;

  // ok
  import AirbnbStyleGuide from './AirbnbStyleGuide';
  export default AirbnbStyleGuide.es6;

  // best
  import { es6 } from './AirbnbStyleGuide';
  export default es6;
  ```

- Do not use wildcard imports.

  > Why? This makes sure you have a single default export.

  ```javascript
  // bad
  import * as AirbnbStyleGuide from './AirbnbStyleGuide';

  // good
  import AirbnbStyleGuide from './AirbnbStyleGuide';
  ```

- Multiline imports should be indented just like multiline array and object literals.
  
  > Why? The curly braces follow the same indentation rules as every other curly brace block in the style guide, as do the trailing commas.

  ```javascript
  // bad
  import {longNameA, longNameB, longNameC, longNameD, longNameE} from 'path';

  // good
  import {
    longNameA,
    longNameB,
    longNameC,
    longNameD,
    longNameE,
  } from 'path';
  ```


## Iterators and Generators

- Don’t use iterators. Prefer JavaScript’s higher-order functions instead of loops like `for-in` or `for-of`.

  > Why? This enforces our immutable rule. Dealing with pure functions that return values is easier to reason about than side effects.

  > Use `map()` / `every()` / `filter()` / `find()` / `findIndex()` / `reduce()` / `some()` / ... to iterate over arrays, and `Object.keys()` / `Object.values()` / `Object.entries()` to produce arrays so you can iterate over objects.

  ```javascript
  const numbers = [1, 2, 3, 4, 5];

  // bad
  let sum = 0;
  for (let num of numbers) {
    sum += num;
  }
  sum === 15;

  // good
  let sum = 0;
  numbers.forEach((num) => {
    sum += num;
  });
  sum === 15;

  // best (use the functional force)
  const sum = numbers.reduce((total, num) => total + num, 0);
  sum === 15;

  // bad
  const increasedByOne = [];
  for (let i = 0; i < numbers.length; i++) {
    increasedByOne.push(numbers[i] + 1);
  }

  // good
  const increasedByOne = [];
  numbers.forEach((num) => {
    increasedByOne.push(num + 1);
  });

  // best (keeping it functional)
  const increasedByOne = numbers.map((num) => num + 1);
  ```

## Properties

- Use dot notation when accessing properties.
- 
  ```javascript
  const luke = {
    jedi: true,
    age: 28,
  };

  // bad
  const isJedi = luke['jedi'];

  // good
  const isJedi = luke.jedi;
  ```

- Use bracket notation `[]` when accessing properties with a variable.

  ```javascript
  const luke = {
    jedi: true,
    age: 28,
  };

  function getProp(prop) {
    return luke[prop];
  }

  const isJedi = getProp('jedi');
  ```

- Use exponentiation operator `**` when calculating exponentiations.

  ```javascript
  // bad
  const binary = Math.pow(2, 10);

  // good
  const binary = 2 ** 10;
  ```

## Variables

- Always use `const` or `let` to declare variables. Not doing so will result in global variables. We want to avoid polluting the global namespace. 

  ```javascript
  // bad
  superPower = new SuperPower();

  // good
  const superPower = new SuperPower();
  ```

- Use one `const` or `let` declaration per variable or assignment.

  > Why? It’s easier to add new variable declarations this way, and you never have to worry about swapping out a `;` for a `,` or introducing punctuation-only diffs. You can also step through each declaration with the debugger, instead of jumping through all of them at once.

  ```javascript
  // bad
  const items = getItems(),
      goSportsTeam = true,
      dragonball = 'z';

  // bad
  // (compare to above, and try to spot the mistake)
  const items = getItems(),
      goSportsTeam = true;
      dragonball = 'z';

  // good
  const items = getItems();
  const goSportsTeam = true;
  const dragonball = 'z';
  ```

- Group all your `const`s and then group all your `let`s.

  > Why? This is helpful when later on you might need to assign a variable depending on one of the previously assigned variables.

  ```javascript
  // bad
  let i, len, dragonball,
      items = getItems(),
      goSportsTeam = true;

  // bad
  let i;
  const items = getItems();
  let dragonball;
  const goSportsTeam = true;
  let len;

  // good
  const goSportsTeam = true;
  const items = getItems();
  let dragonball;
  let i;
  let length;
  ```

- Assign variables where you need them, but place them in a reasonable place.

  > Why? `let` and `const` are block scoped and not function scoped.

  ```javascript
  // bad - unnecessary function call
  function checkName(hasName) {
    const name = getName();

    if (hasName === 'test') {
      return false;
    }

    if (name === 'test') {
      this.setName('');
      return false;
    }

    return name;
  }

  // good
  function checkName(hasName) {
    if (hasName === 'test') {
      return false;
    }

    const name = getName();

    if (name === 'test') {
      this.setName('');
      return false;
    }

    return name;
  }
  ```

## Comparison Operators & Equality

- Use `===` and `!==` over `==` and `!=`. eslint: [`eqeqeq`](https://eslint.org/docs/rules/eqeqeq)

- Conditional statements such as the `if` statement evaluate their expression using coercion with the `ToBoolean` abstract method and always follow these simple rules:

    - **Objects** evaluate to **true**
    - **Undefined** evaluates to **false**
    - **Null** evaluates to **false**
    - **Booleans** evaluate to **the value of the boolean**
    - **Numbers** evaluate to **false** if **+0, -0, or NaN**, otherwise **true**
    - **Strings** evaluate to **false** if an empty string `''`, otherwise **true**

  ```javascript
  if ([0] && []) {
    // true
    // an array (even an empty one) is an object, objects will evaluate to true
  }
  ```

- Use shortcuts for booleans, but explicit comparisons for strings and numbers.

  ```javascript
  // bad
  if (isValid === true) {
    // ...
  }

  // good
  if (isValid) {
    // ...
  }

  // bad
  if (name) {
    // ...
  }

  // good
  if (name !== '') {
    // ...
  }

  // bad
  if (collection.length) {
    // ...
  }

  // good
  if (collection.length > 0) {
    // ...
  }
  ```

- Ternaries should not be nested and generally be single line expressions.

  ```javascript
  // bad
  const foo = maybe1 > maybe2
    ? "bar"
    : value1 > value2 ? "baz" : null;

  // split into 2 separated ternary expressions
  const maybeNull = value1 > value2 ? 'baz' : null;

  // better
  const foo = maybe1 > maybe2
    ? 'bar'
    : maybeNull;

  // best
  const foo = maybe1 > maybe2 ? 'bar' : maybeNull;
  ```

- Avoid unneeded ternary statements. 

  ```javascript
  // bad
  const foo = a ? a : b;
  const bar = c ? true : false;
  const baz = c ? false : true;
  const quux = a != null ? a : b;

  // good
  const foo = a || b;
  const bar = !!c;
  const baz = !c;
  const quux = a ?? b;
  ```

- When mixing operators, enclose them in parentheses. The only exception is the standard arithmetic operators: `+`, `-`, and `**` since their precedence is broadly understood. We recommend enclosing `/` and `*` in parentheses because their precedence can be ambiguous when they are mixed.
  
  > Why? This improves readability and clarifies the developer’s intention.

  ```javascript
  // bad
  const foo = a && b < 0 || c > 0 || d + 1 === 0;

  // bad
  const bar = a ** b - 5 % d;

  // bad
  // one may be confused into thinking (a || b) && c
  if (a || b && c) {
    return d;
  }

  // bad
  const bar = a + b / c * d;

  // good
  const foo = (a && b < 0) || c > 0 || (d + 1 === 0);

  // good
  const bar = a ** b - (5 % d);

  // good
  if (a || (b && c)) {
    return d;
  }

  // good
  const bar = a + (b / c) * d;
  ```

## Blocks

- Use braces with all multiline blocks.

  ```javascript
  // bad
  if (test)
    return false;

  // good
  if (test) return false;

  // good
  if (test) {
    return false;
  }

  // bad
  function foo() { return false; }

  // good
  function bar() {
    return false;
  }
  ```

- If you’re using multiline blocks with `if` and `else`, put `else` on the same line as your `if` block’s closing brace. 

  ```javascript
  // bad
  if (test) {
    thing1();
    thing2();
  }
  else {
    thing3();
  }

  // good
  if (test) {
    thing1();
    thing2();
  } else {
    thing3();
  }
  ```

- If an `if` block always executes a `return` statement, the subsequent `else` block is unnecessary. A `return` in an `else if` block following an `if` block that contains a `return` can be separated into multiple `if` blocks.

  ```javascript
  // bad
  function foo() {
    if (x) {
      return x;
    } else {
      return y;
    }
  }

  // bad
  function cats() {
    if (x) {
      return x;
    } else if (y) {
      return y;
    }
  }

  // bad
  function dogs() {
    if (x) {
      return x;
    } else {
      if (y) {
        return y;
      }
    }
  }

  // good
  function foo() {
    if (x) {
      return x;
    }

    return y;
  }

  // good
  function cats() {
    if (x) {
      return x;
    }

    if (y) {
      return y;
    }
  }

  // good
  function dogs(x) {
    if (x) {
      if (z) {
        return y;
      }
    } else {
      return z;
    }
  }
  ```

## Control Statements

- In case your control statement (`if`, `while` etc.) gets too long or exceeds the maximum line length, each (grouped) condition could be put into a new line. The logical operator should begin the line.

  > Why? Requiring operators at the beginning of the line keeps the operators aligned and follows a pattern similar to method chaining. This also improves readability by making it easier to visually follow complex logic.

  ```javascript
  // bad
  if ((foo === 123 || bar === 'abc') && doesItLookGoodWhenItBecomesThatLong() && isThisReallyHappening()) {
    thing1();
  }

  // bad
  if (foo === 123 &&
    bar === 'abc') {
    thing1();
  }

  // bad
  if (foo === 123
    && bar === 'abc') {
    thing1();
  }

  // bad
  if (
    foo === 123 &&
    bar === 'abc'
  ) {
    thing1();
  }

  // good
  if (
    foo === 123
    && bar === 'abc'
  ) {
    thing1();
  }

  // good
  if (
    (foo === 123 || bar === 'abc')
    && doesItLookGoodWhenItBecomesThatLong()
    && isThisReallyHappening()
  ) {
    thing1();
  }

  // good
  if (foo === 123 && bar === 'abc') {
    thing1();
  }
  ```

## Comments

- Use `/** ... */` for multiline comments.

  ```javascript
  // bad
  // make() returns a new element
  // based on the passed in tag name
  //
  // @param {String} tag
  // @return {Element} element
  function make(tag) {

    // ...

    return element;
  }

  // good
  /**
   * make() returns a new element
   * based on the passed-in tag name
   */
  function make(tag) {

    // ...

    return element;
  }
  ```

- Use `//` for single line comments. Place single line comments on a newline above the subject of the comment. Put an empty line before the comment unless it’s on the first line of a block.

  ```javascript
  // bad
  const active = true;  // is current tab

  // good
  // is current tab
  const active = true;

  // bad
  function getType() {
    console.log('fetching type...');
    // set the default type to 'no type'
    const type = this.type || 'no type';

    return type;
  }

  // good
  function getType() {
    console.log('fetching type...');

    // set the default type to 'no type'
    const type = this.type || 'no type';

    return type;
  }

  // also good
  function getType() {
    // set the default type to 'no type'
    const type = this.type || 'no type';

    return type;
  }
  ```

- Start all comments with a space to make it easier to read. eslint: [`spaced-comment`](https://eslint.org/docs/rules/spaced-comment)

  ```javascript
  // bad
  //is current tab
  const active = true;

  // good
  // is current tab
  const active = true;

  // bad
  /**
   *make() returns a new element
   *based on the passed-in tag name
   */
  function make(tag) {

    // ...

    return element;
  }

  // good
  /**
   * make() returns a new element
   * based on the passed-in tag name
   */
  function make(tag) {

    // ...

    return element;
  }
  ```

- Prefixing your comments with `FIXME` or `TODO` helps other developers quickly understand if you’re pointing out a problem that needs to be revisited, or if you’re suggesting a solution to the problem that needs to be implemented. These are different than regular comments because they are actionable. The actions are `FIXME: -- need to figure this out` or `TODO: -- need to implement`.

- Use `// FIXME:` to annotate problems.

  ```javascript
  class Calculator extends Abacus {
    constructor() {
      super();

      // FIXME: shouldn’t use a global here
      total = 0;
    }
  }
  ```

- Use `// TODO:` to annotate solutions to problems.

  ```javascript
  class Calculator extends Abacus {
    constructor() {
      super();

      // TODO: total should be configurable by an options param
      this.total = 0;
    }
  }
  ```

## Whitespace

- Use soft tabs (space character) set to 2 spaces. 

  ```javascript
  // bad
  function foo() {
  ∙∙∙∙let name;
  }

  // bad
  function bar() {
  ∙let name;
  }

  // good
  function baz() {
  ∙∙let name;
  }
  ```

- Place 1 space before the leading brace. 

  ```javascript
  // bad
  function test(){
    console.log('test');
  }

  // good
  function test() {
    console.log('test');
  }

  // bad
  dog.set('attr',{
    age: '1 year',
    breed: 'Bernese Mountain Dog',
  });

  // good
  dog.set('attr', {
    age: '1 year',
    breed: 'Bernese Mountain Dog',
  });
  ```

- Place 1 space before the opening parenthesis in control statements (`if`, `while` etc.). Place no space between the argument list and the function name in function calls and declarations. eslint: [`keyword-spacing`](https://eslint.org/docs/rules/keyword-spacing)

  ```javascript
  // bad
  if(isJedi) {
    fight ();
  }

  // good
  if (isJedi) {
    fight();
  }

  // bad
  function fight () {
    console.log ('Swooosh!');
  }

  // good
  function fight() {
    console.log('Swooosh!');
  }
  ```

- Set off operators with spaces. 

  ```javascript
  // bad
  const x=y+5;

  // good
  const x = y + 5;
  ```

- Do not pad your blocks with blank lines. 

  ```javascript
  // bad
  function bar() {

    console.log(foo);

  }

  // bad
  if (baz) {

    console.log(quux);
  } else {
    console.log(foo);

  }

  // bad
  class Foo {

    constructor(bar) {
      this.bar = bar;
    }
  }

  // good
  function bar() {
    console.log(foo);
  }

  // good
  if (baz) {
    console.log(quux);
  } else {
    console.log(foo);
  }
  ```

- Do not use multiple blank lines to pad your code. 

  <!-- markdownlint-disable MD012 -->
  ```javascript
  // bad
  class Person {
    constructor(fullName, email, birthday) {
      this.fullName = fullName;


      this.email = email;


      this.setAge(birthday);
    }


    setAge(birthday) {
      const today = new Date();


      const age = this.getAge(today, birthday);


      this.age = age;
    }


    getAge(today, birthday) {
      // ..
    }
  }

  // good
  class Person {
    constructor(fullName, email, birthday) {
      this.fullName = fullName;
      this.email = email;
      this.setAge(birthday);
    }

    setAge(birthday) {
      const today = new Date();
      const age = getAge(today, birthday);
      this.age = age;
    }

    getAge(today, birthday) {
      // ..
    }
  }
  ```

- Do not add spaces inside parentheses. 

  ```javascript
  // bad
  function bar( foo ) {
    return foo;
  }

  // good
  function bar(foo) {
    return foo;
  }

  // bad
  if ( foo ) {
    console.log(foo);
  }

  // good
  if (foo) {
    console.log(foo);
  }
  ```

- Do not add spaces inside brackets.

  ```javascript
  // bad
  const foo = [ 1, 2, 3 ];
  console.log(foo[ 0 ]);

  // good
  const foo = [1, 2, 3];
  console.log(foo[0]);
  ```

- Add spaces inside curly braces. 

  ```javascript
  // bad
  const foo = {clark: 'kent'};

  // good
  const foo = { clark: 'kent' };
  ```

- Avoid spaces between functions and their invocations. 

  ```javascript
  // bad
  func ();

  func
  ();

  // good
  func();
  ```

- Enforce spacing between keys and values in object literal properties. 

  ```javascript
  // bad
  const obj = { foo : 42 };
  const obj2 = { foo:42 };

  // good
  const obj = { foo: 42 };
  ```

## Commas

- Leading commas: **Nope.** 

  ```javascript
  // bad
  const story = [
      once
    , upon
    , aTime
  ];

  // good
  const story = [
    once,
    upon,
    aTime,
  ];

  // bad
  const hero = {
      firstName: 'Ada'
    , lastName: 'Lovelace'
    , birthYear: 1815
    , superPower: 'computers'
  };

  // good
  const hero = {
    firstName: 'Ada',
    lastName: 'Lovelace',
    birthYear: 1815,
    superPower: 'computers',
  };
  ```

-  trailing comma: **Yup.**

  > Why? This leads to cleaner git diffs. Also, transpilers like Babel will remove the additional trailing comma in the transpiled code which means you don’t have to worry about the [trailing comma problem](https://github.com/airbnb/javascript/blob/es5-deprecated/es5/README.md#commas) in legacy browsers.

  ```javascript
  // bad
  const hero = {
    firstName: 'Dana',
    lastName: 'Scully'
  };

  const heroes = [
    'Batman',
    'Superman'
  ];

  // good
  const hero = {
    firstName: 'Dana',
    lastName: 'Scully',
  };

  const heroes = [
    'Batman',
    'Superman',
  ];

  // bad
  function createHero(
    firstName,
    lastName,
    inventorOf
  ) {
    // does nothing
  }

  // good
  function createHero(
    firstName,
    lastName,
    inventorOf,
  ) {
    // does nothing
  }

  // good (note that a comma must not appear after a "rest" element)
  function createHero(
    firstName,
    lastName,
    inventorOf,
    ...heroArgs
  ) {
    // does nothing
  }

  // bad
  createHero(
    firstName,
    lastName,
    inventorOf
  );

  // good
  createHero(
    firstName,
    lastName,
    inventorOf,
  );

  // good (note that a comma must not appear after a "rest" element)
  createHero(
    firstName,
    lastName,
    inventorOf,
    ...heroArgs
  );
  ```

## Semicolons

- semicolons are required **Yup.** 

  > Why? When JavaScript encounters a line break without a semicolon, it uses a set of rules called [Automatic Semicolon Insertion](https://tc39.github.io/ecma262/#sec-automatic-semicolon-insertion) to determine whether it should regard that line break as the end of a statement, and (as the name implies) place a semicolon into your code before the line break if it thinks so. ASI contains a few eccentric behaviors, though, and your code will break if JavaScript misinterprets your line break. These rules will become more complicated as new features become a part of JavaScript. Explicitly terminating your statements and configuring your linter to catch missing semicolons will help prevent you from encountering issues.

  ```javascript
  // bad - raises exception
  const luke = {}
  const leia = {}
  [luke, leia].forEach((jedi) => jedi.father = 'vader')

  // bad - raises exception
  const reaction = "No! That’s impossible!"
  (async function meanwhileOnTheFalcon() {
    // handle `leia`, `lando`, `chewie`, `r2`, `c3p0`
    // ...
  }())

  // bad - returns `undefined` instead of the value on the next line - always happens when `return` is on a line by itself because of ASI!
  function foo() {
    return
      'search your feelings, you know it to be foo'
  }

  // good
  const luke = {};
  const leia = {};
  [luke, leia].forEach((jedi) => {
    jedi.father = 'vader';
  });

  // good
  const reaction = 'No! That’s impossible!';
  (async function meanwhileOnTheFalcon() {
    // handle `leia`, `lando`, `chewie`, `r2`, `c3p0`
    // ...
  }());

  // good
  function foo() {
    return 'search your feelings, you know it to be foo';
  }
  ```

  [Read more](https://stackoverflow.com/questions/7365172/semicolon-before-self-invoking-function/7365214#7365214).

**[⬆ back to top](#table-of-contents)**

## Type Casting & Coercion

<a name="coercion--explicit"></a><a name="21.1"></a>
- [22.1](#coercion--explicit) Perform type coercion at the beginning of the statement.

<a name="coercion--strings"></a><a name="21.2"></a>
- [22.2](#coercion--strings) Strings: eslint: [`no-new-wrappers`](https://eslint.org/docs/rules/no-new-wrappers)

  ```javascript
  // => this.reviewScore = 9;

  // bad
  const totalScore = new String(this.reviewScore); // typeof totalScore is "object" not "string"

  // bad
  const totalScore = this.reviewScore + ''; // invokes this.reviewScore.valueOf()

  // bad
  const totalScore = this.reviewScore.toString(); // isn’t guaranteed to return a string

  // good
  const totalScore = String(this.reviewScore);
  ```

<a name="coercion--numbers"></a><a name="21.3"></a>
- [22.3](#coercion--numbers) Numbers: Use `Number` for type casting and `parseInt` always with a radix for parsing strings. eslint: [`radix`](https://eslint.org/docs/rules/radix) [`no-new-wrappers`](https://eslint.org/docs/rules/no-new-wrappers)

  > Why? The `parseInt` function produces an integer value dictated by interpretation of the contents of the string argument according to the specified radix. Leading whitespace in string is ignored. If radix is `undefined` or `0`, it is assumed to be `10` except when the number begins with the character pairs `0x` or `0X`, in which case a radix of 16 is assumed. This differs from ECMAScript 3, which merely discouraged (but allowed) octal interpretation. Many implementations have not adopted this behavior as of 2013. And, because older browsers must be supported, always specify a radix.

  ```javascript
  const inputValue = '4';

  // bad
  const val = new Number(inputValue);

  // bad
  const val = +inputValue;

  // bad
  const val = inputValue >> 0;

  // bad
  const val = parseInt(inputValue);

  // good
  const val = Number(inputValue);

  // good
  const val = parseInt(inputValue, 10);
  ```

<a name="coercion--comment-deviations"></a><a name="21.4"></a>
- [22.4](#coercion--comment-deviations) If for whatever reason you are doing something wild and `parseInt` is your bottleneck and need to use Bitshift for [performance reasons](https://web.archive.org/web/20200414205431/https://jsperf.com/coercion-vs-casting/3), leave a comment explaining why and what you’re doing.

  ```javascript
  // good
  /**
   * parseInt was the reason my code was slow.
   * Bitshifting the String to coerce it to a
   * Number made it a lot faster.
   */
  const val = inputValue >> 0;
  ```

<a name="coercion--bitwise"></a><a name="21.5"></a>
- [22.5](#coercion--bitwise) **Note:** Be careful when using bitshift operations. Numbers are represented as [64-bit values](https://es5.github.io/#x4.3.19), but bitshift operations always return a 32-bit integer ([source](https://es5.github.io/#x11.7)). Bitshift can lead to unexpected behavior for integer values larger than 32 bits. [Discussion](https://github.com/airbnb/javascript/issues/109). Largest signed 32-bit Int is 2,147,483,647:

  ```javascript
  2147483647 >> 0; // => 2147483647
  2147483648 >> 0; // => -2147483648
  2147483649 >> 0; // => -2147483647
  ```

<a name="coercion--booleans"></a><a name="21.6"></a>
- [22.6](#coercion--booleans) Booleans: eslint: [`no-new-wrappers`](https://eslint.org/docs/rules/no-new-wrappers)

  ```javascript
  const age = 0;

  // bad
  const hasAge = new Boolean(age);

  // good
  const hasAge = Boolean(age);

  // best
  const hasAge = !!age;
  ```

**[⬆ back to top](#table-of-contents)**

## Naming Conventions

<a name="naming--descriptive"></a><a name="22.1"></a>
- [23.1](#naming--descriptive) Avoid single letter names. Be descriptive with your naming. eslint: [`id-length`](https://eslint.org/docs/rules/id-length)

  ```javascript
  // bad
  function q() {
    // ...
  }

  // good
  function query() {
    // ...
  }
  ```

<a name="naming--camelCase"></a><a name="22.2"></a>
- [23.2](#naming--camelCase) Use camelCase when naming objects, functions, and instances. eslint: [`camelcase`](https://eslint.org/docs/rules/camelcase)

  ```javascript
  // bad
  const OBJEcttsssss = {};
  const this_is_my_object = {};
  function c() {}

  // good
  const thisIsMyObject = {};
  function thisIsMyFunction() {}
  ```

<a name="naming--PascalCase"></a><a name="22.3"></a>
- [23.3](#naming--PascalCase) Use PascalCase only when naming constructors or classes. eslint: [`new-cap`](https://eslint.org/docs/rules/new-cap)

  ```javascript
  // bad
  function user(options) {
    this.name = options.name;
  }

  const bad = new user({
    name: 'nope',
  });

  // good
  class User {
    constructor(options) {
      this.name = options.name;
    }
  }

  const good = new User({
    name: 'yup',
  });
  ```

<a name="naming--leading-underscore"></a><a name="22.4"></a>
- [23.4](#naming--leading-underscore) Do not use trailing or leading underscores. eslint: [`no-underscore-dangle`](https://eslint.org/docs/rules/no-underscore-dangle)

  > Why? JavaScript does not have the concept of privacy in terms of properties or methods. Although a leading underscore is a common convention to mean “private”, in fact, these properties are fully public, and as such, are part of your public API contract. This convention might lead developers to wrongly think that a change won’t count as breaking, or that tests aren’t needed. tl;dr: if you want something to be “private”, it must not be observably present.

  ```javascript
  // bad
  this.__firstName__ = 'Panda';
  this.firstName_ = 'Panda';
  this._firstName = 'Panda';

  // good
  this.firstName = 'Panda';

  // good, in environments where WeakMaps are available
  // see https://kangax.github.io/compat-table/es6/#test-WeakMap
  const firstNames = new WeakMap();
  firstNames.set(this, 'Panda');
  ```

<a name="naming--self-this"></a><a name="22.5"></a>
- [23.5](#naming--self-this) Don’t save references to `this`. Use arrow functions or [Function#bind](https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Function/bind).

  ```javascript
  // bad
  function foo() {
    const self = this;
    return function () {
      console.log(self);
    };
  }

  // bad
  function foo() {
    const that = this;
    return function () {
      console.log(that);
    };
  }

  // good
  function foo() {
    return () => {
      console.log(this);
    };
  }
  ```

<a name="naming--filename-matches-export"></a><a name="22.6"></a>
- [23.6](#naming--filename-matches-export) A base filename should exactly match the name of its default export.

  ```javascript
  // file 1 contents
  class CheckBox {
    // ...
  }
  export default CheckBox;

  // file 2 contents
  export default function fortyTwo() { return 42; }

  // file 3 contents
  export default function insideDirectory() {}

  // in some other file
  // bad
  import CheckBox from './checkBox'; // PascalCase import/export, camelCase filename
  import FortyTwo from './FortyTwo'; // PascalCase import/filename, camelCase export
  import InsideDirectory from './InsideDirectory'; // PascalCase import/filename, camelCase export

  // bad
  import CheckBox from './check_box'; // PascalCase import/export, snake_case filename
  import forty_two from './forty_two'; // snake_case import/filename, camelCase export
  import inside_directory from './inside_directory'; // snake_case import, camelCase export
  import index from './inside_directory/index'; // requiring the index file explicitly
  import insideDirectory from './insideDirectory/index'; // requiring the index file explicitly

  // good
  import CheckBox from './CheckBox'; // PascalCase export/import/filename
  import fortyTwo from './fortyTwo'; // camelCase export/import/filename
  import insideDirectory from './insideDirectory'; // camelCase export/import/directory name/implicit "index"
  // ^ supports both insideDirectory.js and insideDirectory/index.js
  ```

<a name="naming--camelCase-default-export"></a><a name="22.7"></a>
- [23.7](#naming--camelCase-default-export) Use camelCase when you export-default a function. Your filename should be identical to your function’s name.

  ```javascript
  function makeStyleGuide() {
    // ...
  }

  export default makeStyleGuide;
  ```

<a name="naming--PascalCase-singleton"></a><a name="22.8"></a>
- [23.8](#naming--PascalCase-singleton) Use PascalCase when you export a constructor / class / singleton / function library / bare object.

  ```javascript
  const AirbnbStyleGuide = {
    es6: {
    },
  };

  export default AirbnbStyleGuide;
  ```

<a name="naming--Acronyms-and-Initialisms"></a>
- [23.9](#naming--Acronyms-and-Initialisms) Acronyms and initialisms should always be all uppercased, or all lowercased.

  > Why? Names are for readability, not to appease a computer algorithm.

  ```javascript
  // bad
  import SmsContainer from './containers/SmsContainer';

  // bad
  const HttpRequests = [
    // ...
  ];

  // good
  import SMSContainer from './containers/SMSContainer';

  // good
  const HTTPRequests = [
    // ...
  ];

  // also good
  const httpRequests = [
    // ...
  ];

  // best
  import TextMessageContainer from './containers/TextMessageContainer';

  // best
  const requests = [
    // ...
  ];
  ```

<a name="naming--uppercase"></a>
- [23.10](#naming--uppercase) You may optionally uppercase a constant only if it (1) is exported, (2) is a `const` (it can not be reassigned), and (3) the programmer can trust it (and its nested properties) to never change.

  > Why? This is an additional tool to assist in situations where the programmer would be unsure if a variable might ever change. UPPERCASE_VARIABLES are letting the programmer know that they can trust the variable (and its properties) not to change.
    - What about all `const` variables? - This is unnecessary, so uppercasing should not be used for constants within a file. It should be used for exported constants however.
    - What about exported objects? - Uppercase at the top level of export (e.g. `EXPORTED_OBJECT.key`) and maintain that all nested properties do not change.

  ```javascript
  // bad
  const PRIVATE_VARIABLE = 'should not be unnecessarily uppercased within a file';

  // bad
  export const THING_TO_BE_CHANGED = 'should obviously not be uppercased';

  // bad
  export let REASSIGNABLE_VARIABLE = 'do not use let with uppercase variables';

  // ---

  // allowed but does not supply semantic value
  export const apiKey = 'SOMEKEY';

  // better in most cases
  export const API_KEY = 'SOMEKEY';

  // ---

  // bad - unnecessarily uppercases key while adding no semantic value
  export const MAPPING = {
    KEY: 'value'
  };

  // good
  export const MAPPING = {
    key: 'value',
  };
  ```

**[⬆ back to top](#table-of-contents)**

## Accessors

<a name="accessors--not-required"></a><a name="23.1"></a>
- [24.1](#accessors--not-required) Accessor functions for properties are not required.

<a name="accessors--no-getters-setters"></a><a name="23.2"></a>
- [24.2](#accessors--no-getters-setters) Do not use JavaScript getters/setters as they cause unexpected side effects and are harder to test, maintain, and reason about. Instead, if you do make accessor functions, use `getVal()` and `setVal('hello')`.

  ```javascript
  // bad
  class Dragon {
    get age() {
      // ...
    }

    set age(value) {
      // ...
    }
  }

  // good
  class Dragon {
    getAge() {
      // ...
    }

    setAge(value) {
      // ...
    }
  }
  ```

<a name="accessors--boolean-prefix"></a><a name="23.3"></a>
- [24.3](#accessors--boolean-prefix) If the property/method is a `boolean`, use `isVal()` or `hasVal()`.

  ```javascript
  // bad
  if (!dragon.age()) {
    return false;
  }

  // good
  if (!dragon.hasAge()) {
    return false;
  }
  ```

<a name="accessors--consistent"></a><a name="23.4"></a>
- [24.4](#accessors--consistent) It’s okay to create `get()` and `set()` functions, but be consistent.

  ```javascript
  class Jedi {
    constructor(options = {}) {
      const lightsaber = options.lightsaber || 'blue';
      this.set('lightsaber', lightsaber);
    }

    set(key, val) {
      this[key] = val;
    }

    get(key) {
      return this[key];
    }
  }
  ```

**[⬆ back to top](#table-of-contents)**

## Events

<a name="events--hash"></a><a name="24.1"></a>
- [25.1](#events--hash) When attaching data payloads to events (whether DOM events or something more proprietary like Backbone events), pass an object literal (also known as a "hash") instead of a raw value. This allows a subsequent contributor to add more data to the event payload without finding and updating every handler for the event. For example, instead of:

  ```javascript
  // bad
  $(this).trigger('listingUpdated', listing.id);

  // ...

  $(this).on('listingUpdated', (e, listingID) => {
    // do something with listingID
  });
  ```

  prefer:

  ```javascript
  // good
  $(this).trigger('listingUpdated', { listingID: listing.id });

  // ...

  $(this).on('listingUpdated', (e, data) => {
    // do something with data.listingID
  });
  ```

**[⬆ back to top](#table-of-contents)**

## jQuery

<a name="jquery--dollar-prefix"></a><a name="25.1"></a>
- [26.1](#jquery--dollar-prefix) Prefix jQuery object variables with a `$`.

  ```javascript
  // bad
  const sidebar = $('.sidebar');

  // good
  const $sidebar = $('.sidebar');

  // good
  const $sidebarBtn = $('.sidebar-btn');
  ```

<a name="jquery--cache"></a><a name="25.2"></a>
- [26.2](#jquery--cache) Cache jQuery lookups.

  ```javascript
  // bad
  function setSidebar() {
    $('.sidebar').hide();

    // ...

    $('.sidebar').css({
      'background-color': 'pink',
    });
  }

  // good
  function setSidebar() {
    const $sidebar = $('.sidebar');
    $sidebar.hide();

    // ...

    $sidebar.css({
      'background-color': 'pink',
    });
  }
  ```

<a name="jquery--queries"></a><a name="25.3"></a>
- [26.3](#jquery--queries) For DOM queries use Cascading `$('.sidebar ul')` or parent > child `$('.sidebar > ul')`. [jsPerf](https://web.archive.org/web/20200414183810/https://jsperf.com/jquery-find-vs-context-sel/16)

<a name="jquery--find"></a><a name="25.4"></a>
- [26.4](#jquery--find) Use `find` with scoped jQuery object queries.

  ```javascript
  // bad
  $('ul', '.sidebar').hide();

  // bad
  $('.sidebar').find('ul').hide();

  // good
  $('.sidebar ul').hide();

  // good
  $('.sidebar > ul').hide();

  // good
  $sidebar.find('ul').hide();
  ```

**[⬆ back to top](#table-of-contents)**

## ECMAScript 5 Compatibility

<a name="es5-compat--kangax"></a><a name="26.1"></a>
- [27.1](#es5-compat--kangax) Refer to [Kangax](https://twitter.com/kangax/)’s ES5 [compatibility table](https://kangax.github.io/es5-compat-table/).

**[⬆ back to top](#table-of-contents)**

<a name="ecmascript-6-styles"></a>
## ECMAScript 6+ (ES 2015+) Styles

<a name="es6-styles"></a><a name="27.1"></a>
- [28.1](#es6-styles) This is a collection of links to the various ES6+ features.

1. [Arrow Functions](#arrow-functions)
1. [Classes](#classes--constructors)
1. [Object Shorthand](#es6-object-shorthand)
1. [Object Concise](#es6-object-concise)
1. [Object Computed Properties](#es6-computed-properties)
1. [Template Strings](#es6-template-literals)
1. [Destructuring](#destructuring)
1. [Default Parameters](#es6-default-parameters)
1. [Rest](#es6-rest)
1. [Array Spreads](#es6-array-spreads)
1. [Let and Const](#references)
1. [Exponentiation Operator](#es2016-properties--exponentiation-operator)
1. [Iterators and Generators](#iterators-and-generators)
1. [Modules](#modules)

<a name="tc39-proposals"></a>
- [28.2](#tc39-proposals) Do not use [TC39 proposals](https://github.com/tc39/proposals) that have not reached stage 3.

  > Why? [They are not finalized](https://tc39.github.io/process-document/), and they are subject to change or to be withdrawn entirely. We want to use JavaScript, and proposals are not JavaScript yet.

**[⬆ back to top](#table-of-contents)**

## Standard Library

The [Standard Library](https://developer.mozilla.org/en/docs/Web/JavaScript/Reference/Global_Objects)
contains utilities that are functionally broken but remain for legacy reasons.

<a name="standard-library--isnan"></a>
- [29.1](#standard-library--isnan) Use `Number.isNaN` instead of global `isNaN`.
  eslint: [`no-restricted-globals`](https://eslint.org/docs/rules/no-restricted-globals)

  > Why? The global `isNaN` coerces non-numbers to numbers, returning true for anything that coerces to NaN.
  > If this behavior is desired, make it explicit.

  ```javascript
  // bad
  isNaN('1.2'); // false
  isNaN('1.2.3'); // true

  // good
  Number.isNaN('1.2.3'); // false
  Number.isNaN(Number('1.2.3')); // true
  ```

<a name="standard-library--isfinite"></a>
- [29.2](#standard-library--isfinite) Use `Number.isFinite` instead of global `isFinite`.
  eslint: [`no-restricted-globals`](https://eslint.org/docs/rules/no-restricted-globals)

  > Why? The global `isFinite` coerces non-numbers to numbers, returning true for anything that coerces to a finite number.
  > If this behavior is desired, make it explicit.

  ```javascript
  // bad
  isFinite('2e3'); // true

  // good
  Number.isFinite('2e3'); // false
  Number.isFinite(parseInt('2e3', 10)); // true
  ```

**[⬆ back to top](#table-of-contents)**

## Testing

<a name="testing--yup"></a><a name="28.1"></a>
- [30.1](#testing--yup) **Yup.**

  ```javascript
  function foo() {
    return true;
  }
  ```

<a name="testing--for-real"></a><a name="28.2"></a>
- [30.2](#testing--for-real) **No, but seriously**:
    - Whichever testing framework you use, you should be writing tests!
    - Strive to write many small pure functions, and minimize where mutations occur.
    - Be cautious about stubs and mocks - they can make your tests more brittle.
    - We primarily use [`mocha`](https://www.npmjs.com/package/mocha) and [`jest`](https://www.npmjs.com/package/jest) at Airbnb. [`tape`](https://www.npmjs.com/package/tape) is also used occasionally for small, separate modules.
    - 100% test coverage is a good goal to strive for, even if it’s not always practical to reach it.
    - Whenever you fix a bug, *write a regression test*. A bug fixed without a regression test is almost certainly going to break again in the future.

**[⬆ back to top](#table-of-contents)**

## Performance

- [On Layout & Web Performance](https://www.kellegous.com/j/2013/01/26/layout-performance/)
- [String vs Array Concat](https://web.archive.org/web/20200414200857/https://jsperf.com/string-vs-array-concat/2)
- [Try/Catch Cost In a Loop](https://web.archive.org/web/20200414190827/https://jsperf.com/try-catch-in-loop-cost/12)
- [Bang Function](https://web.archive.org/web/20200414205426/https://jsperf.com/bang-function)
- [jQuery Find vs Context, Selector](https://web.archive.org/web/20200414200850/https://jsperf.com/jquery-find-vs-context-sel/164)
- [innerHTML vs textContent for script text](https://web.archive.org/web/20200414205428/https://jsperf.com/innerhtml-vs-textcontent-for-script-text)
- [Long String Concatenation](https://web.archive.org/web/20200414203914/https://jsperf.com/ya-string-concat/38)
- [Are JavaScript functions like `map()`, `reduce()`, and `filter()` optimized for traversing arrays?](https://www.quora.com/JavaScript-programming-language-Are-Javascript-functions-like-map-reduce-and-filter-already-optimized-for-traversing-array/answer/Quildreen-Motta)
- Loading...

**[⬆ back to top](#table-of-contents)**

## Resources

**Learning ES6+**

- [Latest ECMA spec](https://tc39.github.io/ecma262/)
- [ExploringJS](https://exploringjs.com/)
- [ES6 Compatibility Table](https://kangax.github.io/compat-table/es6/)
- [Comprehensive Overview of ES6 Features](http://es6-features.org/)
- [JavaScript Roadmap](https://roadmap.sh/javascript)

**Read This**

- [Standard ECMA-262](https://www.ecma-international.org/ecma-262/6.0/index.html)

**Tools**

- Code Style Linters
    - [ESlint](https://eslint.org/) - [Airbnb Style .eslintrc](https://github.com/airbnb/javascript/blob/master/linters/.eslintrc)
    - [JSHint](https://jshint.com/) - [Airbnb Style .jshintrc](https://github.com/airbnb/javascript/blob/master/linters/.jshintrc)
- Neutrino Preset - [@neutrinojs/airbnb](https://neutrinojs.org/packages/airbnb/)

**Other Style Guides**

- [Google JavaScript Style Guide](https://google.github.io/styleguide/jsguide.html)
- [Google JavaScript Style Guide (Old)](https://google.github.io/styleguide/javascriptguide.xml)
- [jQuery Core Style Guidelines](https://contribute.jquery.org/style-guide/js/)
- [Principles of Writing Consistent, Idiomatic JavaScript](https://github.com/rwaldron/idiomatic.js)
- [StandardJS](https://standardjs.com)

**Other Styles**

- [Naming this in nested functions](https://gist.github.com/cjohansen/4135065) - Christian Johansen
- [Conditional Callbacks](https://github.com/airbnb/javascript/issues/52) - Ross Allen
- [Popular JavaScript Coding Conventions on GitHub](http://sideeffect.kr/popularconvention/#javascript) - JeongHoon Byun
- [Multiple var statements in JavaScript, not superfluous](https://benalman.com/news/2012/05/multiple-var-statements-javascript/) - Ben Alman

**Further Reading**

- [Understanding JavaScript Closures](https://javascriptweblog.wordpress.com/2010/10/25/understanding-javascript-closures/) - Angus Croll
- [Basic JavaScript for the impatient programmer](https://www.2ality.com/2013/06/basic-javascript.html) - Dr. Axel Rauschmayer
- [You Might Not Need jQuery](https://youmightnotneedjquery.com/) - Zack Bloom & Adam Schwartz
- [ES6 Features](https://github.com/lukehoban/es6features) - Luke Hoban
- [Frontend Guidelines](https://github.com/bendc/frontend-guidelines) - Benjamin De Cock

**Books**

- [JavaScript: The Good Parts](https://www.amazon.com/JavaScript-Good-Parts-Douglas-Crockford/dp/0596517742) - Douglas Crockford
- [JavaScript Patterns](https://www.amazon.com/JavaScript-Patterns-Stoyan-Stefanov/dp/0596806752) - Stoyan Stefanov
- [Pro JavaScript Design Patterns](https://www.amazon.com/JavaScript-Design-Patterns-Recipes-Problem-Solution/dp/159059908X) - Ross Harmes and Dustin Diaz
- [High Performance Web Sites: Essential Knowledge for Front-End Engineers](https://www.amazon.com/High-Performance-Web-Sites-Essential/dp/0596529309) - Steve Souders
- [Maintainable JavaScript](https://www.amazon.com/Maintainable-JavaScript-Nicholas-C-Zakas/dp/1449327680) - Nicholas C. Zakas
- [JavaScript Web Applications](https://www.amazon.com/JavaScript-Web-Applications-Alex-MacCaw/dp/144930351X) - Alex MacCaw
- [Pro JavaScript Techniques](https://www.amazon.com/Pro-JavaScript-Techniques-John-Resig/dp/1590597273) - John Resig
- [Smashing Node.js: JavaScript Everywhere](https://www.amazon.com/Smashing-Node-js-JavaScript-Everywhere-Magazine/dp/1119962595) - Guillermo Rauch
- [Secrets of the JavaScript Ninja](https://www.amazon.com/Secrets-JavaScript-Ninja-John-Resig/dp/193398869X) - John Resig and Bear Bibeault
- [Human JavaScript](http://humanjavascript.com/) - Henrik Joreteg
- [Superhero.js](http://superherojs.com/) - Kim Joar Bekkelund, Mads Mobæk, & Olav Bjorkoy
- [JSBooks](https://jsbooks.revolunet.com/) - Julien Bouquillon
- [Third Party JavaScript](https://www.manning.com/books/third-party-javascript) - Ben Vinegar and Anton Kovalyov
- [Effective JavaScript: 68 Specific Ways to Harness the Power of JavaScript](https://amzn.com/dp/0321812182) - David Herman
- [Eloquent JavaScript](https://eloquentjavascript.net/) - Marijn Haverbeke
- [You Don’t Know JS: ES6 & Beyond](https://shop.oreilly.com/product/0636920033769.do) - Kyle Simpson

**Blogs**

- [JavaScript Weekly](https://javascriptweekly.com/)
- [JavaScript, JavaScript...](https://javascriptweblog.wordpress.com/)
- [Bocoup Weblog](https://bocoup.com/weblog)
- [Adequately Good](https://www.adequatelygood.com/)
- [NCZOnline](https://www.nczonline.net/)
- [Perfection Kills](http://perfectionkills.com/)
- [Ben Alman](https://benalman.com/)
- [Dmitry Baranovskiy](http://dmitry.baranovskiy.com/)
- [nettuts](https://code.tutsplus.com/?s=javascript)

**Podcasts**

- [JavaScript Air](https://javascriptair.com/)
- [JavaScript Jabber](https://devchat.tv/js-jabber/)

**[⬆ back to top](#table-of-contents)**

## In the Wild

This is a list of organizations that are using this style guide. Send us a pull request and we'll add you to the list.

- **123erfasst**: [123erfasst/javascript](https://github.com/123erfasst/javascript)
- **4Catalyzer**: [4Catalyzer/javascript](https://github.com/4Catalyzer/javascript)
- **Aan Zee**: [AanZee/javascript](https://github.com/AanZee/javascript)
- **Airbnb**: [airbnb/javascript](https://github.com/airbnb/javascript)
- **AloPeyk**: [AloPeyk](https://github.com/AloPeyk)
- **AltSchool**: [AltSchool/javascript](https://github.com/AltSchool/javascript)
- **Apartmint**: [apartmint/javascript](https://github.com/apartmint/javascript)
- **Ascribe**: [ascribe/javascript](https://github.com/ascribe/javascript)
- **Avant**: [avantcredit/javascript](https://github.com/avantcredit/javascript)
- **Axept**: [axept/javascript](https://github.com/axept/javascript)
- **Billabong**: [billabong/javascript](https://github.com/billabong/javascript)
- **Bisk**: [bisk](https://github.com/Bisk/)
- **Bonhomme**: [bonhommeparis/javascript](https://github.com/bonhommeparis/javascript)
- **Brainshark**: [brainshark/javascript](https://github.com/brainshark/javascript)
- **CaseNine**: [CaseNine/javascript](https://github.com/CaseNine/javascript)
- **Cerner**: [Cerner](https://github.com/cerner/)
- **Chartboost**: [ChartBoost/javascript-style-guide](https://github.com/ChartBoost/javascript-style-guide)
- **Coeur d'Alene Tribe**: [www.cdatribe-nsn.gov](https://www.cdatribe-nsn.gov)
- **ComparaOnline**: [comparaonline/javascript](https://github.com/comparaonline/javascript-style-guide)
- **Compass Learning**: [compasslearning/javascript-style-guide](https://github.com/compasslearning/javascript-style-guide)
- **DailyMotion**: [dailymotion/javascript](https://github.com/dailymotion/javascript)
- **DoSomething**: [DoSomething/eslint-config](https://github.com/DoSomething/eslint-config)
- **Digitpaint** [digitpaint/javascript](https://github.com/digitpaint/javascript)
- **Drupal**: [www.drupal.org](https://git.drupalcode.org/project/drupal/blob/8.6.x/core/.eslintrc.json)
- **Ecosia**: [ecosia/javascript](https://github.com/ecosia/javascript)
- **Evernote**: [evernote/javascript-style-guide](https://github.com/evernote/javascript-style-guide)
- **Evolution Gaming**: [evolution-gaming/javascript](https://github.com/evolution-gaming/javascript)
- **EvozonJs**: [evozonjs/javascript](https://github.com/evozonjs/javascript)
- **ExactTarget**: [ExactTarget/javascript](https://github.com/ExactTarget/javascript)
- **Flexberry**: [Flexberry/javascript-style-guide](https://github.com/Flexberry/javascript-style-guide)
- **Gawker Media**: [gawkermedia](https://github.com/gawkermedia/)
- **General Electric**: [GeneralElectric/javascript](https://github.com/GeneralElectric/javascript)
- **Generation Tux**: [GenerationTux/javascript](https://github.com/generationtux/styleguide)
- **GoodData**: [gooddata/gdc-js-style](https://github.com/gooddata/gdc-js-style)
- **GreenChef**: [greenchef/javascript](https://github.com/greenchef/javascript)
- **Grooveshark**: [grooveshark/javascript](https://github.com/grooveshark/javascript)
- **Grupo-Abraxas**: [Grupo-Abraxas/javascript](https://github.com/Grupo-Abraxas/javascript)
- **Happeo**: [happeo/javascript](https://github.com/happeo/javascript)
- **Honey**: [honeyscience/javascript](https://github.com/honeyscience/javascript)
- **How About We**: [howaboutwe/javascript](https://github.com/howaboutwe/javascript-style-guide)
- **HubSpot**: [HubSpot/javascript](https://github.com/HubSpot/javascript)
- **Hyper**: [hyperoslo/javascript-playbook](https://github.com/hyperoslo/javascript-playbook/blob/master/style.md)
- **InterCity Group**: [intercitygroup/javascript-style-guide](https://github.com/intercitygroup/javascript-style-guide)
- **Jam3**: [Jam3/Javascript-Code-Conventions](https://github.com/Jam3/Javascript-Code-Conventions)
- **JSSolutions**: [JSSolutions/javascript](https://github.com/JSSolutions/javascript)
- **Kaplan Komputing**: [kaplankomputing/javascript](https://github.com/kaplankomputing/javascript)
- **KickorStick**: [kickorstick](https://github.com/kickorstick/)
- **Kinetica Solutions**: [kinetica/javascript](https://github.com/kinetica/Javascript-style-guide)
- **LEINWAND**: [LEINWAND/javascript](https://github.com/LEINWAND/javascript)
- **Lonely Planet**: [lonelyplanet/javascript](https://github.com/lonelyplanet/javascript)
- **M2GEN**: [M2GEN/javascript](https://github.com/M2GEN/javascript)
- **Mighty Spring**: [mightyspring/javascript](https://github.com/mightyspring/javascript)
- **MinnPost**: [MinnPost/javascript](https://github.com/MinnPost/javascript)
- **MitocGroup**: [MitocGroup/javascript](https://github.com/MitocGroup/javascript)
- **Muber**: [muber](https://github.com/muber/)
- **National Geographic Society**: [natgeosociety](https://github.com/natgeosociety/)
- **NullDev**: [NullDevCo/JavaScript-Styleguide](https://github.com/NullDevCo/JavaScript-Styleguide)
- **Nulogy**: [nulogy/javascript](https://github.com/nulogy/javascript)
- **Orange Hill Development**: [orangehill/javascript](https://github.com/orangehill/javascript)
- **Orion Health**: [orionhealth/javascript](https://github.com/orionhealth/javascript)
- **Peerby**: [Peerby/javascript](https://github.com/Peerby/javascript)
- **Pier 1**: [Pier1/javascript](https://github.com/pier1/javascript)
- **Qotto**: [Qotto/javascript-style-guide](https://github.com/Qotto/javascript-style-guide)
- **React**: [reactjs.org/docs/how-to-contribute.html#style-guide](https://reactjs.org/docs/how-to-contribute.html#style-guide)
- **REI**: [reidev/js-style-guide](https://github.com/rei/code-style-guides/)
- **Ripple**: [ripple/javascript-style-guide](https://github.com/ripple/javascript-style-guide)
- **Sainsbury’s Supermarkets**: [jsainsburyplc](https://github.com/jsainsburyplc)
- **Shutterfly**: [shutterfly/javascript](https://github.com/shutterfly/javascript)
- **Sourcetoad**: [sourcetoad/javascript](https://github.com/sourcetoad/javascript)
- **Springload**: [springload](https://github.com/springload/)
- **StratoDem Analytics**: [stratodem/javascript](https://github.com/stratodem/javascript)
- **SteelKiwi Development**: [steelkiwi/javascript](https://github.com/steelkiwi/javascript)
- **StudentSphere**: [studentsphere/javascript](https://github.com/studentsphere/guide-javascript)
- **SwoopApp**: [swoopapp/javascript](https://github.com/swoopapp/javascript)
- **SysGarage**: [sysgarage/javascript-style-guide](https://github.com/sysgarage/javascript-style-guide)
- **Syzygy Warsaw**: [syzygypl/javascript](https://github.com/syzygypl/javascript)
- **Target**: [target/javascript](https://github.com/target/javascript)
- **Terra**: [terra](https://github.com/cerner?utf8=%E2%9C%93&q=terra&type=&language=)
- **TheLadders**: [TheLadders/javascript](https://github.com/TheLadders/javascript)
- **The Nerdery**: [thenerdery/javascript-standards](https://github.com/thenerdery/javascript-standards)
- **Tomify**: [tomprats](https://github.com/tomprats)
- **Traitify**: [traitify/eslint-config-traitify](https://github.com/traitify/eslint-config-traitify)
- **T4R Technology**: [T4R-Technology/javascript](https://github.com/T4R-Technology/javascript)
- **UrbanSim**: [urbansim](https://github.com/urbansim/)
- **VoxFeed**: [VoxFeed/javascript-style-guide](https://github.com/VoxFeed/javascript-style-guide)
- **WeBox Studio**: [weboxstudio/javascript](https://github.com/weboxstudio/javascript)
- **Weggo**: [Weggo/javascript](https://github.com/Weggo/javascript)
- **Zillow**: [zillow/javascript](https://github.com/zillow/javascript)
- **ZocDoc**: [ZocDoc/javascript](https://github.com/ZocDoc/javascript)

**[⬆ back to top](#table-of-contents)**

## Translation

This style guide is also available in other languages:

- ![br](https://raw.githubusercontent.com/gosquared/flags/master/flags/flags/shiny/24/Brazil.png) **Brazilian Portuguese**: [armoucar/javascript-style-guide](https://github.com/armoucar/javascript-style-guide)
- ![bg](https://raw.githubusercontent.com/gosquared/flags/master/flags/flags/shiny/24/Bulgaria.png) **Bulgarian**: [borislavvv/javascript](https://github.com/borislavvv/javascript)
- ![ca](https://raw.githubusercontent.com/fpmweb/javascript-style-guide/master/img/catala.png) **Catalan**: [fpmweb/javascript-style-guide](https://github.com/fpmweb/javascript-style-guide)
- ![cn](https://raw.githubusercontent.com/gosquared/flags/master/flags/flags/shiny/24/China.png) **Chinese (Simplified)**: [lin-123/javascript](https://github.com/lin-123/javascript)
- ![tw](https://raw.githubusercontent.com/gosquared/flags/master/flags/flags/shiny/24/Taiwan.png) **Chinese (Traditional)**: [jigsawye/javascript](https://github.com/jigsawye/javascript)
- ![fr](https://raw.githubusercontent.com/gosquared/flags/master/flags/flags/shiny/24/France.png) **French**: [nmussy/javascript-style-guide](https://github.com/nmussy/javascript-style-guide)
- ![de](https://raw.githubusercontent.com/gosquared/flags/master/flags/flags/shiny/24/Germany.png) **German**: [timofurrer/javascript-style-guide](https://github.com/timofurrer/javascript-style-guide)
- ![it](https://raw.githubusercontent.com/gosquared/flags/master/flags/flags/shiny/24/Italy.png) **Italian**: [sinkswim/javascript-style-guide](https://github.com/sinkswim/javascript-style-guide)
- ![jp](https://raw.githubusercontent.com/gosquared/flags/master/flags/flags/shiny/24/Japan.png) **Japanese**: [mitsuruog/javascript-style-guide](https://github.com/mitsuruog/javascript-style-guide)
- ![kr](https://raw.githubusercontent.com/gosquared/flags/master/flags/flags/shiny/24/South-Korea.png) **Korean**: [ParkSB/javascript-style-guide](https://github.com/ParkSB/javascript-style-guide)
- ![ru](https://raw.githubusercontent.com/gosquared/flags/master/flags/flags/shiny/24/Russia.png) **Russian**: [leonidlebedev/javascript-airbnb](https://github.com/leonidlebedev/javascript-airbnb)
- ![es](https://raw.githubusercontent.com/gosquared/flags/master/flags/flags/shiny/24/Spain.png) **Spanish**: [paolocarrasco/javascript-style-guide](https://github.com/paolocarrasco/javascript-style-guide)
- ![th](https://raw.githubusercontent.com/gosquared/flags/master/flags/flags/shiny/24/Thailand.png) **Thai**: [lvarayut/javascript-style-guide](https://github.com/lvarayut/javascript-style-guide)
- ![tr](https://raw.githubusercontent.com/gosquared/flags/master/flags/flags/shiny/24/Turkey.png) **Turkish**: [eraycetinay/javascript](https://github.com/eraycetinay/javascript)
- ![ua](https://raw.githubusercontent.com/gosquared/flags/master/flags/flags/shiny/24/Ukraine.png) **Ukrainian**: [ivanzusko/javascript](https://github.com/ivanzusko/javascript)
- ![vn](https://raw.githubusercontent.com/gosquared/flags/master/flags/flags/shiny/24/Vietnam.png) **Vietnam**: [dangkyokhoang/javascript-style-guide](https://github.com/dangkyokhoang/javascript-style-guide)

## The JavaScript Style Guide Guide

- [Reference](https://github.com/airbnb/javascript/wiki/The-JavaScript-Style-Guide-Guide)

## Chat With Us About JavaScript

- Find us on [gitter](https://gitter.im/airbnb/javascript).

## Contributors

- [View Contributors](https://github.com/airbnb/javascript/graphs/contributors)

## License

(The MIT License)

Copyright (c) 2012 Airbnb

Permission is hereby granted, free of charge, to any person obtaining
a copy of this software and associated documentation files (the
'Software'), to deal in the Software without restriction, including
without limitation the rights to use, copy, modify, merge, publish,
distribute, sublicense, and/or sell copies of the Software, and to
permit persons to whom the Software is furnished to do so, subject to
the following conditions:

The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED 'AS IS', WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY
CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE
SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

**[⬆ back to top](#table-of-contents)**

## Amendments

We encourage you to fork this guide and change the rules to fit your team’s style guide. Below, you may list some amendments to the style guide. This allows you to periodically update your style guide without having to deal with merge conflicts.

# };
