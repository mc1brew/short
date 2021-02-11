import React, {Component} from 'react'

class Alert extends Component
{
    renderResult = this.props.renderResult
    redirectUrl = this.props.redirectUrl

    render(){
        const redirectUrl = this.props.redirectUrl

        if(redirectUrl === '')
            return null;

        return(
            <div className="alert alert-success alert-dismissible fade show col-md-6 mx-auto" role="alert">
                <div className="d-flex justify-content-between">
                    <p className="justify"><strong class="text-success">Short link: <a id="redirectUrl" href={redirectUrl}>{redirectUrl}</a></strong></p>
                    <button type="button" className="btn btn-success float-right" aria-label="Copy" onClick= {() => this.copyUrl(redirectUrl)}>Copy</button>
                </div>
            </div>
        )
    }

    async copyUrl(redirectUrl)
    {
        await navigator.clipboard.writeText(redirectUrl);
        this.close();
    }

    close()
    {
        this.renderResult('');
    }
}

export default Alert