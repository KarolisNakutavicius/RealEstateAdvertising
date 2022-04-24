import React, { Component } from 'react'
import Row from 'react-bootstrap/Row';
import Input from "react-validation/build/input";
import ValidationHelper from '../Helpers/ValidationHelper'
import Form from "react-validation/build/form";
import Col from 'react-bootstrap/Col';

export default class Filters extends Component {
    constructor(props) {
        super(props);
        this.onMaxPriceChange = this.onMaxPriceChange.bind(this)
        this.onMinPriceChange = this.onMinPriceChange.bind(this)

        this.state =
        {
            minPrice: 1000,
            maxPrice: 500000
        }
    }

    componentDidUpdate(prevProps) {
        this.props.filtersChanged();
    }

    onMinPriceChange(e) {
        this.setState({
            minPrice: parseInt(e.target.value)
        })

    }

    onMaxPriceChange(e) {
        this.setState({
            maxPrice: parseInt(e.target.value)
        })
    }

    render() {
        return (
            <div style={{position:'relative', backgroundColor:'rgba(210, 215,211,1)', borderRadius:'6px', textAlign:'center'}}>

                <div style={{position:"absolute", right:'5px', top:"5px"}}>
                    <button type="button" class="btn-close" aria-label="Close"></button>
                </div>

                <h3>Filters</h3>

                <Form className="p-3">

                    <div className='d-flex'>
                    <div className='p-2 filter-block'>
                        <h5>Price</h5>
                        <Row className='p-2'>
                            <label for="from" className="col-3">From:</label>
                            <div className='col'>
                                <Input
                                    min="0" max="999999999999" step="1000"
                                    type="number"
                                    className="col-1 mt-0 form-control"
                                    id="from"
                                    value={this.state.minPrice}
                                    onChange={this.onMinPriceChange}
                                />
                            </div>
                        </Row>
                        <Row className='p-2'>
                            <label for="to" className="col-3">To:</label>
                            <div className='col'>
                                <Input
                                    min="5000" max="999999999999" step="1000"
                                    type="number"
                                    className="col-1 mt-0 form-control"
                                    id="to"
                                    value={this.state.maxPrice}
                                    onChange={this.onMaxPriceChange}
                                />
                            </div>
                        </Row>
                    </div>

                    <div className='p-2 filter-block'>
                        <h4>City</h4>
                        <select className='mt-4 form-select'>
                            <option>Vilnius</option>
                        </select>
                    </div>


                    </div>

                   


                </Form>

                <button type="button" className="mb-3 btn btn-primary">Filter down</button>

            </div>
        )
    }
}
