import React, {Component} from 'react'
import AdvertisementService from '../../Services/AdvertisementService'
import Advertisment from './Advertisment';

export default class MyAdvertisments extends Component {

    constructor(props) {
        super(props);

        this.state = {
            advertisements: [],
            message: 'Loading ...'
        }
    }

    async componentDidMount() {
        var ads = await AdvertisementService.getMyAdvertisments();

        if (ads.length > 0) {
            this.setState({
                advertisements: ads
            })

            return;
        }

        this.setState({
            message: "You don't have any advertisements posted"
        })
    }

    render() {
        return (
            <>
                {this.state.advertisements.length == 0 && (
                    <h3>{this.state.message}</h3>
                )}
                <div className='d-flex justify-content-start flex-wrap'>
                    {this.state.advertisements.map(ad => {
                        return <Advertisment ad={ad} isPersonal={false}/>
                    })}
                </div>
            </>
        )
    }
}
