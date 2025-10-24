import { Card, Col, Row } from 'react-bootstrap';
import '.././App.css'
import { useState, useEffect } from 'react';
import { format } from 'date-fns';

function EventsList(props: any) {

    const [data, setData] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        const fetchData = async () => {
            try {
                const response = await fetch(`https://localhost:7151/api/v1.0/event-viewer/events/${props.venueId}`);
                if (!response.ok) {
                    throw new Error(`HTTP error! status: ${response.status}`);
                }
                const result = await response.json();
                console.log(result)
                setData(result);
            } catch (e) {
                setError(error);
            } finally {
                setLoading(false);
            }
        };

        fetchData();
    }, []); // Empty dependency array ensures this runs once on mount

    if (loading) return <p>Loading data...</p>;
    if (error) return <p>Error: {error}</p>;

    return (
        <div>
            <Row xs={1} md={2} lg={3} className="g-4">
                {data.map((event:any, index) => (
                    <Col key={index}>
                        <Card>
                            <Card.Body>
                                <Card.Title>{event.name}</Card.Title>
                                <Card.Text>
                                    {event.description}
                                </Card.Text>
                            </Card.Body>
                            <Card.Footer className="text-muted">{format((new Date(event.startDate)), 'MMMM do, yyyy - HH:mm')}</Card.Footer>
                        </Card>
                    </Col>
                ))}
            </Row>
        </div>
    );
}

export default EventsList

