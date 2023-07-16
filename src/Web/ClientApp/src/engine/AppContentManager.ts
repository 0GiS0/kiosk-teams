import { IAppContentLoader } from "./IAppContentLoader";

export class AppContentManager { 
    
    _delay: number;
    _scope: string;
    _loader: IAppContentLoader;
    _lastUrl: string | undefined;

    constructor(scope: string, delay: number, loader: IAppContentLoader) {
        this._scope = scope;
        this._delay = delay;
        this._loader = loader;
        this._lastUrl = "";
    }

    start(callBack: Function)
    {
        setInterval(() => {
            this._loader.loadCurrentItemUrl(this._scope).then(r=> 
                {
                    if (this._lastUrl !== r?.url) {
                        this._lastUrl = r?.url;
                        callBack(r);
                    }
                });
          }, this._delay);
    }
}

