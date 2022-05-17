import React, {useState, useEffect} from 'react'
import Row from 'react-bootstrap/Row';
import Input from "react-validation/build/input";
import Form from "react-validation/build/form";

export default function Filters({getAds, setFilters, filters}) {
    
    const [cities, setCities] = useState([])
    const [message,setMessage] = useState("")
    
    useEffect(() => {
        fetch("/api/Cities").then(response => {
            response.json().then(data => {
                setCities(data)
            })})
        }, [])
         

    function filterDown() {
        if (filters.minPrice > filters.maxPrice) {
            setMessage('Min price cannot be higher than max price')
            return;
        }
        
        getAds(true);
        
        setMessage("")
    }
    
        return (
            <div style={{
                position: 'relative',
                backgroundColor: 'rgba(210, 215,211,1)',
                borderRadius: '6px',
                textAlign: 'center'
            }}>

                <h3>Filters</h3>

                <Form className="p-3">

                    <div className='d-flex'>
                        <div className='p-2 filter-block'>
                            <h5>Price</h5>
                            <Row className='p-2'>
                                <label htmlFor="from" className="col-3">From:</label>
                                <div className='col'>
                                    <Input
                                        min="0" max="999999999999" step="1000"
                                        type="number"
                                        className="col-1 mt-0 form-control"
                                        id="from"
                                        value={filters.minPrice}
                                        onChange={(e) => setFilters({...filters, minPrice: e.target.value})}
                                    />
                                </div>
                            </Row>
                            <Row className='p-2'>
                                <label htmlFor="to" className="col-3">To:</label>
                                <div className='col'>
                                    <Input
                                        min="5000" max="999999999999" step="1000"
                                        type="number"
                                        className="col-1 mt-0 form-control"
                                        id="to"
                                        value={filters.maxPrice}
                                        onChange={(e) => setFilters({...filters, maxPrice: e.target.value})}
                                    />
                                </div>
                            </Row>
                        </div>
                        <div className='p-2 filter-block'>
                            <h4>City</h4>
                            <select className='mt-4 form-select'
                                    value={filters.cityId}
                                    onChange={(e) => setFilters({...filters, cityId: parseInt(e.target.value)})}>
                                <option value='0'>All</option>
                                {cities.map(c => <option key={c.id} value={c.id}>{c.name}</option>)}
                            </select>
                        </div>

                        <div className='p-2 filter-block'>
                            <h4>Type</h4>
                            <select className='mt-4 form-select'
                                    value={filters.type}
                                    onChange={(e) => setFilters({...filters, type: parseInt(e.target.value)})}>
                                <option value='1'>Residential</option>
                                <option value='2'>Commercial</option>
                                <option value='3'>Industrial</option>
                                <option value='4'>Land</option>
                                <option value='5'>SpecialPurpose</option>
                            </select>
                        </div>

                        <div className='p-2 filter-block'>
                            <h4>Purpose</h4>
                            <select className='mt-4 form-select'
                                    value={filters.isRent}
                                    onChange={(e) => setFilters({...filters, isRent: e.target.value})}>
                                <option value="">All</option>
                                <option value="0">Sell</option>
                                <option value="1">Rent</option>
                            </select>
                        </div>
                        
                    </div>
                </Form>

                {message && (
                    <div className="form-group mt-3">
                        <div className="alert alert-danger" role="alert">
                            {message}
                        </div>
                    </div>
                )}

                <button type="button" className="mb-3 btn btn-primary" onClick={filterDown}>Filter
                    down
                </button>

            </div>
        )
}
