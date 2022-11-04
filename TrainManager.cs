using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvikoOOP {
    internal class TrainManager {

        class Locomotive {
            Person _driver;
            Engine _engine;
            public Engine Engine {
                get { return _engine; }
                set { _engine = value; }
            }
            public Person Driver {
                get { return _driver; }
                set { _driver = value; }
            }

            public Locomotive() {
                _driver = new Person("John", "Doe");
                _engine = new Engine(Engine.EngineType.steam);
            }
            public Locomotive(Person driver, Engine engine) {
                _driver = driver;
                _engine = engine;
            }
            public override string ToString() {
                return $"Locomotive: {_engine.ToString()}, driver: {_driver.ToString()}";
            }
        }
        class Person {
            string _firstName;
            string _lastName;
            public string FirstName {
                get {
                    return _firstName;
                }
                set { _firstName = value; }
            }
            public string LastName {
                get { return _lastName; }
                set { _lastName = value; }
            }
            public Person(string firstName, string lastName) {
                _firstName = firstName;
                _lastName = lastName;
            }
            public override string ToString() {
                return $"{_firstName} {_lastName}";
            }
        }
        class Engine {
            EngineType _type;
            public EngineType Type {
                get { return _type; }
                set { _type = value; }
            }
            public enum EngineType {
                steam,
                diesel,
                electric
            }
            public Engine(EngineType type) {
                _type = type;
            }
            public override string ToString() {
                return $"Engine type: {_type}";
            }
        }
        class Chair {
            bool _isNearWindow = false;
            int _number;
            bool _isReserved = false;
            public bool IsReserved {
                get { return _isReserved; }
                set { _isReserved = value; }
            }
            public int Number {
                get { return _number; }
                set { _number = value; }
            }
            public bool IsNearWindow {
                get { return _isNearWindow; }
                set { _isNearWindow = value; }
            }
            public Chair(int number, bool nearWindow = false) {
                _number = number;
                _isNearWindow = nearWindow;
            }
        }
        class Bed {
            int _number;
            bool _isReserved = false;
            public int Number {
                get { return _number; }
                set { _number = value; }
            }
            public bool IsReserved {
                get { return _isReserved; }
                set { _isReserved = value; }
            }
            public Bed() { }
        }
        class Door {
            double _height;
            double _width;
            public double Height {
                get { return _height; }
                set { _height = value; }
            }
            public double Width {
                get { return _width; }
                set { _width = value; }
            }
            public Door(double height, double width) {
                _height = height;
                _width = width;
            }
        }
        class PersonalWagon : IWagon {
            List<Door> _doorList = new List<Door>();
            List<Chair> _chairList = new List<Chair>();
            int _numberOfChairs;
            public List<Door> DoorList {
                get { return _doorList; }
                set { _doorList = value; }
            }
            public List<Chair> ChairList {
                get { return _chairList; }
                set { _chairList = value; }
            }
            public int NumberOfChairs {
                get { return _numberOfChairs; }
                set { _numberOfChairs = value; }
            }
            public PersonalWagon(int numOfChairs) {
                this.NumberOfChairs = numOfChairs;
                for (int i = 0; i < numOfChairs; i++) {
                    _chairList.Add(new Chair(i));
                }
            }
            public override string ToString() {
                return $"Wagon type: {this.GetType().Name}, number of chairs: {_numberOfChairs}";
            }

        }
        class EconomyWagon : PersonalWagon {
            public EconomyWagon(int numOfChairs) : base(numOfChairs) { }
            public override string ToString() {
                return base.ToString();
            }
        }
        class BusinessWagon : PersonalWagon {
            Person _steward;
            public Person Steward {
                get { return _steward; }
                set { _steward = value; }
            }
            public BusinessWagon(int numOfChairs, Person steward) : base(numOfChairs) {
                _steward = steward;
            }
            public override string ToString() {
                return $"{base.ToString()}, steward: {_steward.FirstName} {_steward.LastName}";
            }
        }
        class NightWagon : PersonalWagon {
            Bed[] _beds;
            int _numOfBeds;
            public Bed[] Beds {
                get { return _beds; }
                set { _beds = value; }
            }
            public int NumberOfBeds {
                get { return _numOfBeds; }
                set { _numOfBeds = value; }
            }
            public NightWagon(int numOfChairs, int numOfBeds) : base(numOfChairs) {
                _numOfBeds = numOfBeds;
                _beds = new Bed[numOfChairs];
                for (int i = 0; i < numOfBeds; i++) {
                    _beds[i] = new Bed();
                }
            }
            public override string ToString() {
                return $"{base.ToString()}, number of beds: {_numOfBeds}";
            }
        }
        class HopperWagon : IWagon {
            double _loadingCapacity;
            public double LoadingCapacity {
                get { return _loadingCapacity; }
                set { _loadingCapacity = value; }
            }
            public HopperWagon(double loadingCapacity) {
                _loadingCapacity = loadingCapacity;
            }
            public override string ToString() {
                return $"Wagon type: {this.GetType().Name}, tonnage: {_loadingCapacity}";
            }
        }
        interface IWagon {
            public void ConnectWagon(Train train) {
                if (train.Locomotive.Engine.Type is Engine.EngineType.steam && train.WagonList.Count >= 5) {
                    Console.WriteLine($"Cannot connect wagon. Train capacity reached. " +
                        $"Train engine type: {train.Locomotive.Engine.Type.ToString()}");
                }
                else {
                    train.WagonList.Add(this);
                }
            }
            public void DisconnectWagon(Train train) {
                if (train.WagonList.Contains(this)) {
                    train.WagonList.Remove(this);
                }
                else {
                    Console.WriteLine($"Cannot disconnect wagon, because its not connected to the selected train");
                }
            }
        }
        class Train {
            Locomotive _loco;
            List<IWagon> _wagonList;
            public Locomotive Locomotive {
                get { return _loco; }
                set { _loco = value; }
            }
            public List<IWagon> WagonList {
                get { return _wagonList; }
                set { _wagonList = value; }
            }
            public Train() {
                _wagonList = new List<IWagon>();
                _loco = new Locomotive();
            }
            public Train(Locomotive loco) : this() {
                _loco = loco;
            }
            public Train(Locomotive loco, List<IWagon> wagonList) : this(loco) {
                _wagonList = wagonList;
            }
            public void ConnectWagon(IWagon wagon) {
                wagon.ConnectWagon(this);
            }
            public void DisconnectWagon(IWagon wagon) {
                wagon.DisconnectWagon(this);
            }
            public void ReserveChair(int wagon, int chair) {
                IWagon _wagon = _wagonList[wagon];
                if (_wagon is not PersonalWagon) {
                    Console.WriteLine($"Cannot reserve seat in {_wagon.ToString()}");
                }
                else {
                    PersonalWagon perWagon = (PersonalWagon)_wagon;
                    if (perWagon.ChairList.Count < chair) {
                        int chairCount = perWagon.ChairList.Count;
                        Console.WriteLine($"Chair number: {chair} is not available. There is only {chairCount} chairs in the" +
                            $"{perWagon.GetType().Name}");
                    }
                    else {
                        if (perWagon.ChairList[chair-1].IsReserved) {
                            Console.WriteLine($"Chair number: {chair} in wagon number: {wagon} is already reserved");
                        }
                        else {
                            perWagon.ChairList[chair-1].IsReserved = true;
                        }
                    }
                }
            }
            public void ListReservedChairs() {
                string str = "Number of reserved chairs: ";
                int chairCount = 0;
                foreach (IWagon wagons in _wagonList) {
                    if (wagons is PersonalWagon) {
                        PersonalWagon persWagon = (PersonalWagon)wagons;
                        foreach (Chair chairSelect in persWagon.ChairList) {
                            if (chairSelect.IsReserved) {
                                chairCount++;
                            }
                        }
                    }
                }
                str += $"{chairCount}";
                Console.WriteLine(str);
            }
            public override string ToString() {
                string str = $"Train:\n{_loco.ToString()}\n";
                foreach (var wagon in _wagonList) {
                    str += $"{wagon.ToString()}\n";
                }
                return str;
            }
        }

        public static void MainX() {
            BusinessWagon businessW1 = new BusinessWagon(30, new Person("Lenka", "Kozakova"));
            NightWagon nightW1 = new NightWagon(20, 20);
            HopperWagon hopper1 = new HopperWagon(40);
            Locomotive loco1 = new Locomotive(new Person("Karel", "Novak"), new Engine(Engine.EngineType.diesel));
            Train train1 = new Train(loco1, new List<IWagon> { businessW1, nightW1, hopper1 });
            HopperWagon hopper2 = new HopperWagon(40);
            train1.ConnectWagon(hopper2);

            EconomyWagon economyW1 = new EconomyWagon(60);
            EconomyWagon economyW2 = new EconomyWagon(60);
            BusinessWagon businessW2 = new BusinessWagon(30, new Person("Tereza", "Opozdilova"));
            NightWagon nightW2 = new NightWagon(20, 20);
            HopperWagon hopper3 = new HopperWagon(50);
            Locomotive loco2 = new Locomotive(new Person("Pavel", "Hlavek"), new Engine(Engine.EngineType.steam));
            Train train2 = new Train(loco2, new List<IWagon> { economyW1, economyW2, businessW2, nightW2, hopper3 });
            EconomyWagon economyW3 = new EconomyWagon(60);
            train2.ConnectWagon(economyW3);
            Console.WriteLine(train1.ToString());
            Console.WriteLine(train2.ToString());

            train2.ReserveChair(3, 3);
            train2.ReserveChair(3, 4);
            train2.ReserveChair(3, 5);
            train2.ReserveChair(3, 6);
            train2.ReserveChair(3, 7);
            train2.ReserveChair(3, 3);
            train2.ReserveChair(3, 20);
            train2.ReserveChair(3, 21);
            train2.ReserveChair(4, 3);
            train2.ListReservedChairs();

            train2.DisconnectWagon(hopper2);
            train2.DisconnectWagon(hopper3);
            Console.WriteLine(train1.ToString());
            Console.WriteLine(train2.ToString());


        }

    }
}
