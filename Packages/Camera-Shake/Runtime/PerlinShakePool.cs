using System.Collections.Generic;
using UnityEngine;

namespace CameraShake
{
    public class PerlinShakePool
    {
        private const int POOL_SIZE = 32;

        private PerlinShake[] _shakes;
        private PerlinShake.Params[] _params;
        private Envelope.EnvelopeParams[] _envelopeParams;

        private int _indexerShake;
        private int _indexerParams;
        private int _indexerEnvelopeParams;

        public PerlinShake GetShake()
        {
            if (_indexerShake >= POOL_SIZE)
                _indexerShake = 0;

            return _shakes[_indexerShake++];
        }

        public PerlinShake.Params GetParams()
        {
            if (_indexerParams >= POOL_SIZE)
                _indexerParams = 0;

            return _params[_indexerParams++];
        }
        public Envelope.EnvelopeParams GetEnvelopeParams()
        {
            if (_indexerEnvelopeParams >= POOL_SIZE)
                _indexerEnvelopeParams = 0;

            return _envelopeParams[_indexerEnvelopeParams++];
        }

        public void InitPool()
        {
            _shakes = new PerlinShake[POOL_SIZE];
            _params = new PerlinShake.Params[POOL_SIZE];
            _envelopeParams = new Envelope.EnvelopeParams[POOL_SIZE];

            for (int i = 0; i < POOL_SIZE; i++)
            {
                _shakes[i] = new PerlinShake();

                PerlinShake.Params param = new();
                param.noiseModes = new PerlinShake.NoiseMode[2];
                _params[i] = param;

                _envelopeParams[i] = new Envelope.EnvelopeParams();
            }
        }
    }
}