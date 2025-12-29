import React, { useState } from 'react'
import './App.css'

function App() {
  const [activeTab, setActiveTab] = useState('direct')

  const features = [
    {
      id: 'direct',
      title: 'Direct Copy',
      description: '同一プロジェクト内のSkinnedMeshRenderer間で即座に値を同期します。',
      icon: '🎨'
    },
    {
      id: 'json',
      title: 'JSON Export/Import',
      description: 'プロジェクトを跨いだ改変の受け渡しをJSONファイル経由で。',
      icon: '📂'
    },
    {
      id: 'smart',
      title: 'Smart Matching',
      description: 'BlendShape名を自動検知。メッシュのトポロジが違っても名前が合えばコピー可能。',
      icon: '🔄'
    }
  ]

  return (
    <div className="container">
      <header className="hero fade-in">
        <div className="glow-container">
          <div className="glow primary"></div>
          <div className="glow secondary"></div>
        </div>
        
        <nav className="nav glass">
          <div className="logo gradient-text">BlendShape Copy Tool</div>
          <div className="version">v1.0.0</div>
        </nav>

        <section className="hero-content">
          <h1 className="title">
            VRChatアバター編集を、<br />
            <span className="gradient-text">もっとスムーズに。</span>
          </h1>
          <p className="subtitle">
            直感的な操作で、BlendShapeの値を一瞬でコピー。
            プロジェクトを超えた改変作業を効率化します。
          </p>
          <div className="cta-group">
            <button className="btn primary">Get Started</button>
            <button className="btn outline">Read Docs</button>
          </div>
        </section>
      </header>

      <main className="main-content">
        <section className="features">
          <h2 className="section-title">Features</h2>
          <div className="feature-grid">
            {features.map(f => (
              <div key={f.id} className="feature-card glass">
                <div className="feature-icon">{f.icon}</div>
                <h3 className="feature-name">{f.title}</h3>
                <p className="feature-desc">{f.description}</p>
              </div>
            ))}
          </div>
        </section>

        <section className="guide section-padding">
          <h2 className="section-title">Quick Start Guide</h2>
          <div className="guide-container glass">
            <div className="guide-tabs">
              {['Installation', 'Direct Copy', 'Inter-Project'].map(tab => (
                <button 
                  key={tab} 
                  className={`tab-btn ${activeTab === tab ? 'active' : ''}`}
                  onClick={() => setActiveTab(tab)}
                >
                  {tab}
                </button>
              ))}
            </div>
            <div className="guide-content">
              {activeTab === 'Installation' && (
                <div className="guide-step fade-in">
                  <h3>ALCOMでの導入</h3>
                  <ol>
                    <li>ALCOMを起動しプロジェクトを選択</li>
                    <li>「Add Local Package」から本パッケージを指定</li>
                    <li>Unityメニューの「Window &gt; Avatar Tools」から起動</li>
                  </ol>
                </div>
              )}
              {activeTab === 'Direct Copy' && (
                <div className="guide-step fade-in">
                  <h3>同一プロジェクト内でのコピー</h3>
                  <p>SourceとTargetにメッシュをアサインし、ボタンを押すだけで完了。</p>
                </div>
              )}
              {activeTab === 'Inter-Project' && (
                <div className="guide-step fade-in">
                  <h3>プロジェクトを跨ぐコピー</h3>
                  <p>ExportボタンでJSONを保存し、別のプロジェクトでImportするだけ。</p>
                </div>
              )}
            </div>
          </div>
        </section>
      </main>

      <footer className="footer section-padding">
        <p>© 2025 Antigravity. High Performance Editor Tools for Unity.</p>
      </footer>
    </div>
  )
}

export default App
