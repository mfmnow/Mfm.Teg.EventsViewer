import { Button, Card, Col, Row } from 'react-bootstrap';
import '.././App.css'
import { useState, useEffect } from 'react';
import { format } from 'date-fns';
import DetailsModal from './DetailsModal';

interface Event {
    id: number;
    name: string;
    startDate: Date;
    description: string;
}

interface EventsListProps {
    venueId: number;
    name: string;
}

function EventsList(props: EventsListProps) {

    const [data, setData] = useState<Event[]>([]);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState("");

    const [show, setShow] = useState(false);
    const [name, setName] = useState("");
    const [description, setDescription] = useState("");

    const handleClose = () => setShow(false);
    const handleShow = () => setShow(true);

    useEffect(() => {
        const fetchData = async () => {
            try {
                setLoading(true);
                const response = await fetch(`https://localhost:7151/api/v1.0/event-viewer/events/${props.venueId}`);
                if (!response.ok) {
                    throw new Error(`HTTP error! status: ${response.status}`);
                }
                const result = await response.json();
                setData(result);
            } catch (e) {
                setError(`Cannot load Events: ${e}`);
            } finally {
                setLoading(false);
            }
        };
        fetchData();
    }, []); // Empty dependency array ensures this runs once on mount

    if (loading) return <p>Loading data...</p>;
    if (error != "") return <p>{error}</p>;

    return (
        <div>
            <h5>{props.name}</h5>
            <Row xs={1} md={2} lg={2} xl={3} className="g-4">
                {data.map((event: Event, index) => (
                    <Col key={index}>
                        <Card className="card">
                            <Card.Body>
                                <h6>{event.name}</h6>
                                <Card.Text>
                                    {format(event.startDate, 'MMMM do, yyyy - HH:mm')}                                  
                                </Card.Text>
                                {event.description && event.description.length > 0 && <Button onClick={() => {
                                    setName(event.name);
                                    setDescription(event.description);
                                    handleShow();
                                }}>Show Details</Button>}
                            </Card.Body>
                        </Card>
                    </Col>
                ))}
                <DetailsModal name={name}
                    details={description}
                    show={show}
                    handleClose={handleClose}></DetailsModal>
            </Row>
        </div>
    );
}

export default EventsList

