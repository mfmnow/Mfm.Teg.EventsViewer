import { Container, Navbar } from 'react-bootstrap';
import './App.css'
import VenuesList from './components/VenuesList';

function App() {

    return (
        <div>
            <Navbar className="bg-body-tertiary">
                <Container>
                    <Navbar.Brand href="#home">
                        <img
                            alt=""
                            src="/teg.svg"
                            width="30"
                            height="30"
                            className="d-inline-block align-top"
                        />{' '}
                        TEG Portal
                    </Navbar.Brand>
                </Container>
            </Navbar>
            <Container className="p-3">
                <Container className="p-5 mb-4 bg-light rounded-3">
                    <h4 className="header">Events Viewer</h4>
                    <VenuesList />
                </Container>
            </Container>
        </div>
    );
}

export default App

