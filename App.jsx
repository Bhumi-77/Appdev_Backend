import SearchCustomer from "./pages/staff/SearchCustomer";
import CustomerDetails from "./pages/staff/CustomerDetails";

function App() {
    return (
        <div>
            <SearchCustomer />
            <CustomerDetails id={1} />
        </div>
    );
}
function App1() {

    useEffect(() => {
        axios.get("/customers")
            .then(res => console.log(res.data))
            .catch(err => console.error(err));
    }, []);

    return <h1>Frontend Connected</h1>;
}

export default App;