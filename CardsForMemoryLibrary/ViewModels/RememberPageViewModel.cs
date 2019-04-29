using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CardsForMemoryLibrary.IServices;
using CardsForMemoryLibrary.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace CardsForMemoryLibrary.ViewModels {
    public class RememberPageViewModel : ViewModelBase {
        /// <summary>
        ///     卡片服务
        /// </summary>
        private IPackageService _packageService;

        /// <summary>
        ///     复习服务
        /// </summary>
        private IReviseService _reviseService;

        /// <summary>
        ///     反馈服务
        /// </summary>
        private IFeedbackService _feedbackService;

        /// <summary>
        ///     构造函数
        /// </summary>
        public RememberPageViewModel(IPackageService packageService,
            IReviseService reviseService,
            IFeedbackService feedbackService) {
            _packageService = packageService;
            _reviseService = reviseService;
            _feedbackService = feedbackService;
        }
        /// <summary>
        ///     初始化
        /// </summary>
        public async Task init(List<int> packages, int num) {
            _examSet = await _reviseService.SmartRevise(packages, num);
            //按键全锁定
            if (_examSet.Count == 0)
                Question = "卡包中没有题";
            else {
                _currentCard = 0;
                Question = _examSet[_currentCard].Question;
                //解锁反馈按钮
            }
        }

        //数据绑定
        private string _question;
        private string _answer;

        //内部变量
        private List<Card> _examSet;
        private int _currentCard;


        public string Question {
            get => _question;
            set => Set(nameof(Question), ref _question, value);
        }

        public string Answer {
            get => _answer;
            set => Set(nameof(Answer), ref _answer, value);
        }

        //命令绑定
        private RelayCommand _easyCommand;
        private RelayCommand _normalCommand;
        private RelayCommand _difficultCommand;
        private RelayCommand _next;

        public RelayCommand EasyCommand =>
            _easyCommand ?? (_easyCommand =
                new RelayCommand(() => {
                    //锁定反馈按钮
                    //解锁下一题按钮
                    _feedbackService.Utility(_examSet[_currentCard], Level.Easy);
                    Answer = _examSet[_currentCard].Answer;
                }));

        public RelayCommand NormalCommand =>
            _normalCommand ?? (_normalCommand =
                new RelayCommand(() => {
                    //锁定反馈按钮
                    //解锁下一题按钮
                    _feedbackService.Utility(_examSet[_currentCard], Level.Normal);
                    Answer = _examSet[_currentCard].Answer;
                }));

        public RelayCommand DifficultCommand =>
            _difficultCommand ?? (_difficultCommand =
                new RelayCommand(() => {
                    //锁定反馈按钮
                    //解锁下一题按钮
                    _feedbackService.Utility(_examSet[_currentCard], Level.Difficult);
                    Answer = _examSet[_currentCard].Answer;
                }));

        public RelayCommand Next =>
            _next ?? (_next = new RelayCommand(() => {
                Answer = "";
                //锁定下一题按钮
                if (_currentCard >= _examSet.Count - 1)
                    Question = "做完了！";
                else {
                    _currentCard++;
                    Question = _examSet[_currentCard].Question;
                    //解锁反馈按钮
                }
            }));
    }
}
