class Parent {
    constructor(name, age) {
        this.age = age;
        this.name = name;
    }
    fuckYou() {
        console.log("Tao là " + this.name + ", năm nay tao " + "tuổi");
    }
}

class Child extends Parent {
    constructor(name, age, hobby) {
        super(name, age);
        this.hobby = hobby;
    }
    fuckYou() {
        super.fuckYou();
        console.log("Con là " + this.name + ", năm nay con " + this.age + "tuổi và con thik chơi " + this.hobby);
    }
}

var p1 = new Parent("A",69);
var p2 = new Child("B",96,"máy bay");
p2.fuckYou();

// class Cat { 
//   constructor(name) {
//     this.name = name;
//   }
  
//   speak() {
//     console.log(this.name + ' makes a noise.');
//   }
// }

// class Lion extends Cat {
//   speak() {
//     super.speak();
//     console.log(this.name + ' roars.');
//   }
// }

// var l = new Lion('Fuzzy');
// l.speak(); 
// // Fuzzy makes a noise.
// // Fuzzy roars.