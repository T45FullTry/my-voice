// Audio recording using browser MediaRecorder API
let mediaRecorder = null;
let audioChunks = [];

window.startAudioRecording = function (dotNetHelper) {
    return navigator.mediaDevices.getUserMedia({ audio: true })
        .then(stream => {
            mediaRecorder = new MediaRecorder(stream);
            audioChunks = [];

            mediaRecorder.ondataavailable = event => {
                audioChunks.push(event.data);
            };

            mediaRecorder.onstop = () => {
                const audioBlob = new Blob(audioChunks, { type: 'audio/webm' });
                const reader = new FileReader();
                
                reader.onloadend = function() {
                    const base64data = reader.result;
                    // Remove data URL prefix
                    const base64 = base64data.split(',')[1];
                    const binaryString = atob(base64);
                    const bytes = new Uint8Array(binaryString.length);
                    for (let i = 0; i < binaryString.length; i++) {
                        bytes[i] = binaryString.charCodeAt(i);
                    }
                    
                    // Call back to Blazor
                    dotNetHelper.invokeMethodAsync('OnRecordingComplete', bytes);
                };
                
                reader.readAsDataURL(audioBlob);
            };

            mediaRecorder.start();
        })
        .catch(err => {
            console.error('Error accessing microphone:', err);
            throw err;
        });
};

window.stopAudioRecording = function () {
    return new Promise((resolve, reject) => {
        if (!mediaRecorder || mediaRecorder.state === 'inactive') {
            reject(new Error('Not recording'));
            return;
        }

        mediaRecorder.onstop = () => {
            const audioBlob = new Blob(audioChunks, { type: 'audio/webm' });
            const reader = new FileReader();
            
            reader.onloadend = function() {
                const base64data = reader.result;
                const base64 = base64data.split(',')[1];
                const binaryString = atob(base64);
                const bytes = new Uint8Array(binaryString.length);
                for (let i = 0; i < binaryString.length; i++) {
                    bytes[i] = binaryString.charCodeAt(i);
                }
                resolve(bytes);
            };
            
            reader.onerror = reject;
            reader.readAsDataURL(audioBlob);
        };

        mediaRecorder.stop();
        
        // Stop all tracks
        mediaRecorder.stream.getTracks().forEach(track => track.stop());
    });
};

window.testMicrophoneAccess = function () {
    return navigator.mediaDevices.getUserMedia({ audio: true })
        .then(stream => {
            stream.getTracks().forEach(track => track.stop());
            return true;
        })
        .catch(err => {
            console.error('Microphone access denied:', err);
            return false;
        });
};
